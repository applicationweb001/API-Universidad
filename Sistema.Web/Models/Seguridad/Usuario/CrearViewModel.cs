using Sistema.Web.Models.Seguridad.Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class CrearViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El valor de idrol es inválido")]
        public int idrol { get; set; }
        [Required]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "El nombre debe usuario deber ser como maxiom 9 caracteres")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public int idalumno { get; set; }
    }
}
