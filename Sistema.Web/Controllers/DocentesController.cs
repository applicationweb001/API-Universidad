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

        // GET: api/Secciones
        [HttpGet]
        public async Task<IEnumerable<DocenteViewModel>> Listar()
        {
            var docentes = await _context.Docentes
            //.Include (s => s.Docente)
            .ToListAsync();

            return docentes.Select(s => new DocenteViewModel
            {
                iddocente = s.iddocente,
            
            }) ;                     

        }

        // GET: api/Secciones/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<DocenteViewModel>> Select()
        {
            var docentes = await _context.Docentes
            //.Include (s => s.Docente)
            .ToListAsync();

            return docentes.Select(s => new DocenteViewModel
            {
                iddocente = s.iddocente,

            });

        }





        // GET: api/Secciones/5
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

        // PUT: api/Secciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
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
                .FirstOrDefaultAsync(s => s.iddocente == model.iddocente);

            if (docente == null)
            {
                return NotFound();
            }

            docente.iddocente = model.iddocente;
            //seccion.iddocente = model.iddocente;


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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Docente>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Docente docente = new Docente
            {
                dni = model.dni
                //iddocente = model.iddocente
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
