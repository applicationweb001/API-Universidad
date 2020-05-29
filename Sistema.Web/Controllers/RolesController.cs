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
using Sistema.Web.Models.RegistrosAc.Rol;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly DBContextSistema _context;

        public RolesController(DBContextSistema context)
        {
            _context = context;
        }
    
        // GET: api/Carreras
        [HttpGet]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(c => new RolViewModel
            {
                idrol = c.idrol,
                nombre = c.nombre
            });
        }

        // GET: api/Carreras/Select
     /*   [HttpGet("select")]
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
     */

        // GET: api/Carreras/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> Mostrar([FromRoute]int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new RolViewModel
            {
                idrol= rol.idrol,
                nombre = rol.nombre
            });
        }

        // PUT: api/Carreras
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            //from body nos permite igualar el objeto JSON al objeto que se esta instanciando
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idrol <= 0)
            {
                return BadRequest();
            }

            var rol = await _context.Roles
                .FirstOrDefaultAsync(c => c.idrol == model.idrol);

            if (rol == null)
            {
                return NotFound();
            }

            //
            rol.nombre = model.nombre;

            ///

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(); //200
        }

        // POST: api/Carreras
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rol>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Rol rol = new Rol
            {
                nombre = model.nombre
            };

            _context.Roles.Add(rol);

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

            var rol = await _context.Roles
                .FirstOrDefaultAsync(c => c.idrol== id); // expresion lambda para validar con lo que esta en la web vs la base de datos

            if (rol == null)
            {
                return NotFound();
            }

          //  carrera.condicion = false;

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
                                .Roles
                                .FirstOrDefaultAsync(c => c.idrol == id); // expresion lambda para validar con lo que esta en la web vs la base de datos

            if (categoria == null)
            {
                return NotFound();
            }

          //  categoria.condicion = true;

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

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.idrol== id);
        }
    

    // DELETE: api/Carreras/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
           
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);

            await _context.SaveChangesAsync();

            return Ok();
        }

        
    }
}
