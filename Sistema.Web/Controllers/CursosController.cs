using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Web.Models.ProgramacionAcademica.Curso;

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
        // GET: api/Cursos
        [HttpGet]
        public async Task<IEnumerable<CursoViewModel>> Listar()
        {
            var cursos = await _context.CursoCarreras
                .Include(c => c.Curso)
                .GroupBy(c => new { c.Curso.idcurso, c.Curso.nombre, c.Curso.condicion, })
                .Select(x => new {
                    x.Key.idcurso,
                    x.Key.nombre,
                    x.Key.condicion,
                    contador = x.Count()
                })
                .ToListAsync();

       
            return cursos.Select(c => new CursoViewModel
            {
                idcurso = c.idcurso,
                nombre = c.nombre,
                condicion = c.condicion,
                carreras = c.contador

            });
        }

        //Get: api/Cursos/Carreras/3
        [HttpGet("carreras/{id}")]
        public async Task<IEnumerable<DCarreraViewModel>> ListarCarreras([FromRoute] int id)
        {
            var detalleCarreras = await _context.CursoCarreras
                .Include(c=>c.Carrera)
                .Where(cu=>cu.idcurso == id)
                .ToListAsync();

            return detalleCarreras.Select(c => new DCarreraViewModel
            {
                idcarrera = c.idcarrera,
                carrera = c.Carrera.nombre 
                
            });
        }

        // POST: api/Cursos
        [HttpPost]
        public async Task<ActionResult<Curso>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Curso curso = new Curso
            {
                nombre = model.nombre,
                condicion = true

            };
            try
            {
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync(); //guardamos el curso que hemos creado para que genere el ID la base de datos

                var id = curso.idcurso;

                foreach(var obj in model.Carreras) //se esta recorriendo el arreglo que le hemos enviado en CrearViewModel
                {
                    CursoCarrera cursoCarrera = new CursoCarrera
                    {
                        idcarrera = obj,
                        idcurso = id
                    };
 
                    _context.CursoCarreras.Add(cursoCarrera); //Guardamos en la tabla intermedia
                }
                await _context.SaveChangesAsync(); //Recuerden poner siempre en ASYNC

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        // PUT: api/Cursos
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            //from body nos permite igualar el objeto JSON al objeto que se esta instanciando
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcurso<= 0)
            {
                return BadRequest();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.idcurso == model.idcurso);

            if (curso == null)
            {
                return NotFound();
            }

            //
            curso.nombre = model.nombre;
            ///

            var DCarreras = await _context.CursoCarreras
              .Where(dc => dc.idcurso == model.idcurso)
              .ToListAsync();
            
            try
            {
                foreach (var obj in DCarreras)
                {
                    _context.CursoCarreras.Remove(obj);

                }
                
                foreach (var obj in model.carreras) //se esta recorriendo el arreglo que le hemos enviado en CrearViewModel
                {
                    CursoCarrera cursoCarrera = new CursoCarrera
                    {
                        idcarrera = obj,
                        idcurso = curso.idcurso
                    };

                    _context.CursoCarreras.Add(cursoCarrera); //Guardamos en la tabla intermedia
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(); //200
        }



        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>> Delete([FromRoute]int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            var DCarreras = await _context.CursoCarreras.Where(dc=>dc.idcurso == id).ToListAsync();


            if (curso == null)
            {
                return NotFound();
            }

            foreach (var obj in DCarreras)
            {
                _context.CursoCarreras.Remove(obj);

            }
            await _context.SaveChangesAsync();
            
            _context.Cursos.Remove(curso);
           
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.idcurso == id);
        }
    }
}