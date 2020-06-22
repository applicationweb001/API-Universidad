using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Seguridad;
using Sistema.Web.Models.Seguridad.Rol;

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

        // GET: api/roles
        [HttpGet]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(c => new RolViewModel
            {
                idrol = c.idrol,
                nombre = c.nombre,
                descripcion = c.descripcion
            
            });
        }

        // GET: api/roles/Select
        [HttpGet("select")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var carreras = await _context.Roles
                .ToListAsync();
            
            return carreras.Select(c => new SelectViewModel
            {
                idrol = c.idrol,    
                nombre = c.nombre,
            });
        }
        

        // GET: api/Roles/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> Mostrar([FromRoute] int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new RolViewModel
            {
                idrol = rol.idrol,
                nombre = rol.nombre
            });
        }
      
        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.idrol == id);
        }
    }
}
