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

        [HttpGet("[action]")]
        public async Task<IEnumerable<AlumnoViewModel>> Select()
        {
            var alumnos = await _context.Alumnos
            //.Include (d => d.Docente)
            .ToListAsync();

            return alumnos.Select(a => new AlumnoViewModel
            {
                idAlumno = a.idAlumno,
                nombre = a.nombre,
                apellido = a.apellido,
                dni = a.dni,
                fechanacimiento = a.fechanacimiento,
                direccion = a.direccion
            });

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
                nombreCarrera = a.carrera.nombre,
                idcarrera = a.idcarrera,
                direccion = a.direccion

            });
        }

        // GET: api/Alumnos/
        [HttpGet("libres")]
        public async Task<IEnumerable<AlumnoViewModel>> AlumnosLibres()
        {
            var alumnos = await _context.Alumnos
                                    .Include(a => a.carrera)
                                    .Where(s => s.idusuario == null)
                                    .ToListAsync();

            return alumnos.Select(a => new AlumnoViewModel
            {

                idAlumno = a.idAlumno,
                nombre = a.nombre,
                apellido = a.apellido,
                dni = a.dni,
                fechanacimiento = a.fechanacimiento,
                nombreCarrera = a.carrera.nombre,
                idcarrera = a.idcarrera,
                direccion = a.direccion

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

        //Get: api/Alumno/Matricula/
        [HttpGet("matricula/{id}")]
        public async Task<IActionResult> MatriculaAlumnoCiclo([FromRoute]int id)
        {
            var matricula = await _context.Matriculas.FirstOrDefaultAsync(i => i.idalumno == id && i.anioacademico == "2020-01");

            if(matricula==null)
            {
                return Ok(new { estado = "Estado: No matriculado" });
            }

            return Ok(new { estado = "Estado: Matriculado", idmatricula = matricula.idmatricula });

        }



        // PUT: api/Alumnos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idAlumno < 0)
            {
                return BadRequest(ModelState);
            }
            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.idAlumno == model.idAlumno);
            
            if (alumno == null)
            {
                return NotFound();
            }
            //
            alumno.nombre = model.nombre;
            alumno.apellido = model.apellido;
            alumno.dni = model.dni;
            alumno.direccion = model.direccion;
            alumno.fechanacimiento = model.fechanacimiento;
            alumno.idcarrera = model.idcarrera;
           

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

        // DELETE: api/Alumnos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Alumno>> DeleteAlumno([FromRoute] int id)
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

            return Ok();
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.idAlumno == id);
        }


    }
}
