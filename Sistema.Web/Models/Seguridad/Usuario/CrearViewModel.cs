using Sistema.Web.Models.Seguridad.Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "La contasenia debe ser mayor a 6 caracteres y menor a 12")]
        public string contrasenia { get; set; }

        public List<RolViewModel> roles { get; set; }

    }
}
