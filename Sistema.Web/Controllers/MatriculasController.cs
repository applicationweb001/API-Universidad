using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sistema.Datos;
using Sistema.Entidades.AdministracionAcademica;
using Sistema.Web.Models.AdministracionAcademica.Matricula;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public MatriculasController(DBContextSistema context)
        {
            _context = context;
        }

        // GET: api/Matriculas
        [HttpGet]
        public async Task<IEnumerable<MatriculaViewModel>> GetMatricula()
        {
            
            var matriculas = await _context.Matriculas.ToListAsync();
       
            return matriculas.Select(c => new MatriculaViewModel
            {
                idmatricula = c.idmatricula,
                idalumno = c.idalumno,
                anioacademico = c.anioacademico
            });
        }

        // GET: api/Matriculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound();
            }

            return matricula;
        }

        // PUT: api/Matriculas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula([FromBody] ActualizarViewModel model)
        {
            //from body nos permite igualar el objeto JSON al objeto que se esta instanciando
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idmatricula <= 0)
            {
                return BadRequest();
            }

            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(c => c.idmatricula == model.idmatricula);

            //
            matricula.anioacademico = model.anioacademico;
            matricula.idalumno = model.idalumno;
            ///

            if (matricula == null)
            {
                return NotFound();
            }

            var DSecciones = await _context.MatriculaSecciones
                .Where(dc => dc.idmatricula == model.idmatricula)
                .ToListAsync();
           

            try
            {    
                foreach(var obj in DSecciones)
                {
                    _context.MatriculaSecciones.Remove(obj);
                }
                foreach(var obj in model.Secciones)
                {
                    MatriculaSeccion matriculaSeccion = new MatriculaSeccion
                    {
                        idseccion = obj,
                        idmatricula = matricula.idmatricula
                    };
                    _context.MatriculaSecciones.Add(matriculaSeccion);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(); //200
        }

        // POST: api/Matriculas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Matricula>> PostMatricula([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Matricula matricula = new Matricula
            {
                anioacademico = model.anioacademico,
                idalumno = model.idalumno

            };
            try
            {
                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync(); //guardamos el curso que hemos creado para que genere el ID la base de datos
                
                var id = matricula.idmatricula;

                foreach(var obj in model.Secciones)
                {
                    MatriculaSeccion matriculaSeccion = new MatriculaSeccion
                    {
                        idseccion=obj,
                        idmatricula=id
                    };
                    _context.MatriculaSecciones.Add(matriculaSeccion);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();

        }

        // DELETE: api/Matriculas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Matricula>> DeleteMatricula(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            var DSecciones = await _context.MatriculaSecciones
                .Where(dc => dc.idmatricula == id)
                .ToListAsync();


            if (matricula == null)
            {
                return NotFound();
            }

            foreach(var obj in DSecciones)
            {
                _context.MatriculaSecciones.Remove(obj);
            }
            await _context.SaveChangesAsync();

            _context.Matriculas.Remove(matricula);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.idmatricula == id);
        }
    }
}
