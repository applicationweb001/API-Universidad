using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Web.Models.ProgramacionAcademica.Seccion;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionesController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public SeccionesController(DBContextSistema context)
        {
            _context = context;
        }

        // GET: api/Secciones
        [HttpGet]
        public async Task<IEnumerable<SeccionViewModel>> Listar()
        {
            var secciones = await _context.Secciones
            .Include (s => s.Curso)
            .Include(s => s.Docente)
            .ToListAsync();

            return secciones.Select(s => new SeccionViewModel
            {
                idseccion = s.idseccion,
                idcurso = s.idcurso,
                nombrecurso = s.Curso.nombre,
                cantidad = s.cantidad,
                iddocente = s.iddocente,
                nombredocente = s.Docente.nombre,
                codigo_seccion = s.codigo_seccion,
                ciclo_academico = s.ciclo_academico,
                alumnos_registrados = s.alumnos_registrados

            }) ;                     

        }

        // GET: api/Secciones/curso/id
        [HttpGet("cursos/{id}")]
        public async Task<IEnumerable<SelectViewModel>> SeccionesCurso([FromRoute]int id)
        {
            var secciones = await _context.Secciones
            .Include (s => s.Curso)
            .Include(s=>s.Docente)
            .Where(s=>s.idcurso==id && s.ciclo_academico =="2020-01")
            .ToListAsync();

            return secciones.Select(s => new SelectViewModel
            {
                idseccion = s.idseccion,
                idcurso = s.idcurso,
                nombreCurso = s.Curso.nombre,
                nombreDocente = s.Docente.nombre,
                codigo_seccion = s.codigo_seccion,
                alumnosDisponibles = s.alumnos_registrados,
                totalCupos = s.cantidad
            });                     

        }

        // GET: api/Secciones/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SeccionViewModel>> Select()
        {
            var secciones = await _context.Secciones
            .Include (s => s.Docente)
            .ToListAsync();

            return secciones.Select(s => new SeccionViewModel
            {
                idseccion = s.idseccion,
                idcurso = s.idcurso,
                cantidad = s.cantidad,
                iddocente = s.iddocente,         
                alumnos_registrados = s.alumnos_registrados,                
            });

        }

        // GET: api/Secciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seccion>> Mostrar([FromRoute]int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);

            if (seccion == null)
            {
                return NotFound();
            }

            return Ok(new SeccionViewModel
            {
                idseccion = seccion.idseccion,
                idcurso = seccion.idcurso,
                cantidad = seccion.cantidad,
                iddocente = seccion.iddocente,
                codigo_seccion = seccion.codigo_seccion,
                ciclo_academico = seccion.ciclo_academico,
                alumnos_registrados = seccion.alumnos_registrados,
            });
        }

        // PUT: api/Secciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idseccion <= 0)
            {
                return BadRequest();
            }

            var seccion = await _context.Secciones
                .FirstOrDefaultAsync(s => s.idseccion == model.idseccion);

            if (seccion == null)
            {
                return NotFound();
            }

            seccion.idcurso = model.idcurso;
            seccion.iddocente = model.iddocente;
            seccion.cantidad = model.cantidad;
            seccion.codigo_seccion = "1";
            seccion.inserted_date = DateTime.Now;
            
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

        // POST: api/Secciones
        [HttpPost]
        public async Task<ActionResult<Seccion>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Seccion seccion = new Seccion
            {
                idcurso = model.idcurso,
                iddocente = model.iddocente,
                cantidad = model.cantidad,
                codigo_seccion ="1",
                ciclo_academico = "2020-01",
                inserted_date = DateTime.Now ,
                alumnos_registrados = 0,

            };
            
            _context.Secciones.Add(seccion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(new { fecha = DateTime.Now });
        }

        // DELETE: api/Secciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seccion>> Borrar([FromRoute] int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);

            if (seccion == null)
            {
                return NotFound();
            }

            _context.Secciones.Remove(seccion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        private bool SeccionExists(int id)
        {
            return _context.Secciones.Any(e => e.idseccion == id);
        }
    }
}
