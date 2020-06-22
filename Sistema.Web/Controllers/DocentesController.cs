using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.AdministracionAcademica;
using Sistema.Web.Models.AdministracionAcademica.Docente;


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public DocentesController(DBContextSistema context)
        {
            _context = context;
        }

        // GET: api/Docentes
        [HttpGet]
        public async Task<IEnumerable<DocenteViewModel>> Listar()
        {
            var docentes = await _context.Docentes
            //.Include (d => d.Docente)
            .ToListAsync();

            return docentes.Select(d => new DocenteViewModel
            {
                iddocente = d.iddocente,
                correo = d.correo,
                apellido = d.apellido,
                dni = d.dni,
                nombre = d.nombre,
            
            }) ;                     

        }

        // GET: api/Docentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Docente>> Mostrar([FromRoute]int id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            return Ok(new DocenteViewModel
            {
                iddocente = docente.iddocente,
            });
        }

        // PUT: api/Docentes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.iddocente <= 0)
            {
                return BadRequest();
            }

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(d => d.iddocente == model.iddocente);

            if (docente == null)
            {
                return NotFound();
            }

            docente.nombre = model.nombre;
            docente.apellido = model.apellido;
            docente.correo = model.correo;
            docente.dni = model.dni;

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

        // POST: api/Docentes
        [HttpPost]
        public async Task<ActionResult<Docente>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Docente docente = new Docente
            {
                nombre = model.nombre,
                apellido = model.apellido,
                correo = model.correo,
                dni = model.dni
                
            };

            _context.Docentes.Add(docente);

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

        // DELETE: api/Secciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Docente>> Borrar([FromRoute] int id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            _context.Docentes.Remove(docente);

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

        private bool DocenteExists(int id)
        {
            return _context.Docentes.Any(e => e.iddocente == id);
        }
    }
}
