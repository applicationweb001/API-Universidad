﻿using System;
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
            var msg = "Se ha creado con éxito el usuario";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.nombre == email))
            {
                return BadRequest("El nombre de usuario ya existe");
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

                if (model.idalumno > 0)
                {
                    var alumno = await _context.Alumnos
                         .FirstOrDefaultAsync(a => a.idAlumno == model.idalumno);
                    if (alumno == null)
                    {
                        return NotFound();
                    }

                    alumno.idusuario = usuario.idusuario;

                    msg = "Se ha asigando el usuario al alumno" + alumno.nombre + alumno.apellido + "satisfactoriamente";

                }

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(msg);
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
                return NotFound("No existe ese usuario");
            }

            var alumno = await _context.Alumnos
                                           .Include(a=>a.carrera)
                                           .FirstOrDefaultAsync(a => a.idusuario == usuario.idusuario);

            if (!VerificarPasswordHash(model.password, usuario.password))
            {
                return NotFound("La contraseña no esta funcionando");
            }
            
            if(usuario.Rol.nombre =="Alumno")
            {
                if (alumno== null)
                {
                    return NotFound("Este usuario no tiene asigando un alumno asi que gg");
                }
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

            if(alumno != null)
            {
                claims.Add(new Claim("idalumno", alumno.idAlumno.ToString()));
                claims.Add(new Claim("idcarrera", alumno.idcarrera.ToString()));
                claims.Add(new Claim("nombreCarrera", alumno.carrera.nombre));
            }

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

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuarios([FromRoute] int id)
        {
            var alumno = await _context.Alumnos
                                        .Include(i => i.usuario)
                                        .FirstOrDefaultAsync(i => i.usuario.idusuario == id);

            var usuario = await _context.Usuarios.FindAsync(id);

            if (alumno != null)
            {
                alumno.idusuario = null;
            }

            _context.Usuarios.Remove(usuario);

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

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }
    }
}
