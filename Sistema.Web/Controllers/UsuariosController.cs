using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.RegistrosAc;
using Sistema.Web.Models.RegistrosAc;
using Sistema.Web.Models.RegistrosAc.Usuario;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly DBContextSistema _context;

        public UsuariosController(DBContextSistema context)
        {
            _context = context;
        }
        // GET: api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {

            var usuarios= await _context.Usuarios.ToListAsync();

            return usuarios.Select(c => new UsuarioViewModel
            {
                idusuario = c.idusuario,
                nombre = c.nombre,
                contrasenia = c.contrasenia,
                
            });

          /*  var usuarios = await _context.Roles
                .Include(c => c.rol)
                .GroupBy(c => new { c.Curso.idcurso, c.Curso.nombre, c.Curso.descripcion, c.Curso.condicion, })
                .Select(x => new {
                    idcurso = x.Key.idcurso,
                    nombre = x.Key.nombre,
                    descripcion = x.Key.descripcion,
                    condicion = x.Key.condicion,
                    contador = x.Count()
                })
                .ToListAsync();

            return usuarios.Select(c => new CursoViewModel
            {
                idcurso = c.idcurso,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion,
                carreras = c.contador

            }); 
    
            */
        }

        // POST: api/Cursos/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult<Usuario>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Usuario usuario= new Usuario
            {
                nombre = model.nombre,
                contrasenia = model.contrasenia,

            };
            //Prueba para commit
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync(); //guardamos el usuurio que hemos creado para que genere el ID la base de datos

                var id = usuario.idusuario;

                foreach(var obj in model.roles) //se esta recorriendo el arreglo que le hemos enviado en CrearViewModel
                {
                    Rol rol= new Rol
                    {
                        idrol= obj.idrol
     
                    };

                    _context.Roles.Add(rol); //Guardamos en la tabla intermedia
                }
                await _context.SaveChangesAsync(); //Recuerden poner siempre en ASYNC

            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }



        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }
    }
}