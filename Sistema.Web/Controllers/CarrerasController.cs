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
using Sistema.Web.Models.RegistrosAc.Carrera;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarrerasController : Controller
    {
        private readonly DBContextSistema _context;

        public CarrerasController(DBContextSistema context)
        {
            _context = context;
        }
        //Get Put Post
        // GET: api/Carreras/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CarreraViewModel>> Listar()
        {
            var carreras = await _context.Carreras.ToListAsync();

            return carreras.Select(c => new CarreraViewModel
            {
                idcarrera = c.idcarrera,
                nombre = c.nombre,
                condicion = c.condicion
            });
        }

        // GET: api/Carreras/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var carreras = await _context.Carreras
                .Where(c => c.condicion == true)
                .ToListAsync();

            return carreras.Select(c => new SelectViewModel
            {
                idcarrera = c.idcarrera,
                nombre = c.nombre,
            });

        }

        // GET: api/Carreras/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Carrera>> Mostrar([FromRoute]int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            return Ok(new CarreraViewModel
            {
                idcarrera = carrera.idcarrera,
                nombre = carrera.nombre,
                condicion = carrera.condicion
            });
        }

        // PUT: api/Carreras/Actualizar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            //from body nos permite igualar el objeto JSON al objeto que se esta instanciando
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcarrera <= 0)
            {
                return BadRequest();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(c => c.idcarrera == model.idcarrera);

            if (carrera == null)
            {
                return NotFound();
            }

            //
            carrera.nombre = model.nombre;

            ///

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

        // POST: api/Carreras/Crear
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("[action]")]
        public async Task<ActionResult<Carrera>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }


            Carrera carrera = new Carrera
            {
                nombre = model.nombre,
                condicion = true
            };

            _context.Carreras.Add(carrera);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT: api/Carreras/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(c => c.idcarrera == id); // expresion lambda para validar con lo que esta en la web vs la base de datos

            if (carrera == null)
            {
                return NotFound();
            }

            carrera.condicion = false;

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

        // PUT: api/Carreras/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context
                                .Carreras
                                .FirstOrDefaultAsync(c => c.idcarrera == id); // expresion lambda para validar con lo que esta en la web vs la base de datos

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.condicion = true;

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

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.idcarrera == id);
        }
    

    // DELETE: api/Carreras/5
    [HttpDelete("{id}")]
        public async Task<ActionResult<Carrera>> DeleteCarrera(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();

            return carrera;
        }

        
    }
}
