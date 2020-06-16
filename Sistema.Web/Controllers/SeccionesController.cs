﻿using System;
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
                //nombreseccion =  s.idseccion + s.Curso.codigo
                idcurso = s.idcurso,
                nombrecurso = s.Curso.nombre,
                cantidad = s.cantidad,
                iddocente = s.iddocente,
                nombredocente = s.Docente.nombre

            }) ;                     

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
                iddocente = s.iddocente
               

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
        public async Task<ActionResult<Seccion>> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) // si los data anotation no se cumplen esto valida que se cumplan sino el request sera detenido
            {
                return BadRequest(ModelState);
            }

            Seccion seccion = new Seccion
            {
                idcurso = model.idcurso,
                iddocente = model.iddocente
            };

            _context.Secciones.Add(seccion);

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
