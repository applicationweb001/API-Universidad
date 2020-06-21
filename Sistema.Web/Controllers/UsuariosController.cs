using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sistema.Datos;
using Sistema.Entidades.Seguridad;
using Sistema.Web.Models.Seguridad.Usuario;


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly DBContextSistema _context;
        private readonly IConfiguration _config;

        public UsuariosController(DBContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        // GET: api/Usuarios/
        [HttpGet]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuarios = await _context.Usuarios
                                .Include(u => u.Rol)
                                .ToListAsync();

            return usuarios.Select(u => new UsuarioViewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.Rol.nombre,
                email = u.nombre
            });
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.nombre == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash);

            Usuario usuario = new Usuario
            {
                idrol = model.idrol,
                nombre = model.email.ToLower(),    
                password = passwordHash,
            };

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        //PUT:api/Usuarios
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            //from body nos permite igual el objeto JSON al objeto que se esta instanciando
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.idusuario == model.idusuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.nombre = model.email;
            usuario.idrol = model.idrol;

            if(model.act_password == true)
            {
                CrearPasswordHash(model.password
                                    , out byte[] passwordHash);
                usuario.password = passwordHash;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Usuarios/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var nombre = model.email.ToLower();

            var usuario = await _context.Usuarios
                                        .Include(u => u.Rol)
                                        .FirstOrDefaultAsync(u => u.nombre == nombre);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.password))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                new Claim(ClaimTypes.Email, nombre),
                new Claim(ClaimTypes.Role, usuario.Rol.nombre ),
                new Claim("idusuario", usuario.idusuario.ToString() ),
                new Claim("rol", usuario.Rol.nombre ),
                new Claim("nombre", usuario.nombre )
            };

            return Ok(
                    new { token = GenerarToken(claims) }
                );
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(_config["Seguridad:Secret"])))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }


        private void CrearPasswordHash(string password, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(_config["Seguridad:Secret"])))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }
    }
}
