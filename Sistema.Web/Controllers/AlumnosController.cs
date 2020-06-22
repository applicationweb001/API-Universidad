using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Sistema.Datos;
using Sistema.Entidades.AdministracionAcademica;
using Sistema.Web.Models.AdministracionAcademica.Alumno;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly DBContextSistema _context;
        private readonly IConfiguration _config;

        public AlumnosController(DBContextSistema context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Alumnos
        [HttpGet]
        public async Task<IEnumerable<AlumnoViewModel>> Listar()
        {
            var alumnos = await _context.Alumnos
                                    .Include(a => a.carrera)
                                    .Include(a => a.usuario)
                                    .ToListAsync();
            return alumnos.Select(a => new AlumnoViewModel
            {
                idAlumno = a.idAlumno,
                nombre = a.nombre,
                apellido = a.apellido,
                dni = a.dni,
                fechanacimiento = a.fechanacimiento,
                nombreCarrera = a.carrera.nombre
            });
        }
        //[HttpGet("[action]")]
        //public async Task<IEnumerable<SelectViewModel>> Select()
        //{
        //    var alumnos = await _context.Alumnos
        //        .ToListAsync();

        //    return alumnos.Select(c => new SelectViewModel
        //    {
        //        idAlumno = c.idAlumno,
        //        nombre = c.nombre
        //    });

        //}

        // GET: api/Alumnos/5
        [HttpPost]
        public async Task<ActionResult<Alumno>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState); }

            Alumno alumno = new Alumno
            {
                nombre = model.nombre,
                apellido = model.apellido,
                dni = model.dni,
                direccion = model.direccion,
                idcarrera = model.idcarrera,
                fechanacimiento = model.fechaNacimiento
            };
            _context.Alumnos.Add(alumno);

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


        // PUT: api/Alumnos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idusuario <= 0)
            {
                return BadRequest(ModelState);
            }
            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.idcarrera == model.idcarrera);
            if (alumno == null)
            {
                return NotFound();
            }
            //
            alumno.nombre = model.nombre;

            var DAlumnos = await _context.Carreras
                .Where(da => da.idcarrera == model.idcarrera)
                .ToListAsync();
            return Ok();
        }

        // DELETE: api/Alumnos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Alumno>> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return alumno;
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.idAlumno == id);
        }
    }
}
