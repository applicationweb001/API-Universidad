using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.RegistrosAc;
using Sistema.Web.Models.RegistrosAc.Curso;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public CursosController(DBContextSistema context)
        {
            _context = context;
        }
        // GET: api/Cursos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CursoViewModel>> Listar()
        {
            var cursos = await _context.CursoCarreras
                .Include(c => c.Curso)
                .GroupBy(c => new { c.Curso.idcurso, c.Curso.nombre, c.Curso.descripcion, c.Curso.condicion, })
                .Select(x => new {
                    idcurso = x.Key.idcurso,
                    nombre = x.Key.nombre,
                    descripcion = x.Key.descripcion,
                    condicion = x.Key.condicion,
                    contador = x.Count()
                })
                .ToListAsync();

            return cursos.Select(c => new CursoViewModel
            {
                idcurso = c.idcurso,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion,
                carreras = c.contador

            });
        }

        // POST: api/Cursos/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult<Curso>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Curso curso = new Curso
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true

            };

            try
            {
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync(); //guardamos el curso que hemos creado para que genere el ID la base de datos

                var id = curso.idcurso;

                foreach(var obj in model.cursoCarreras) //se esta recorriendo el arreglo que le hemos enviado en CrearViewModel
                {
                    CursoCarrera cursoCarrera = new CursoCarrera
                    {
                        idcarrera = obj.idcarrera,
                        idcurso = obj.idcurso
                    };

                    _context.CursoCarreras.Add(cursoCarrera); //Guardamos en la tabla intermedia
                }
                await _context.SaveChangesAsync(); //Recuerden poner siempre en ASYNC

            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }



        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return curso;
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.idcurso == id);
        }
    }
}