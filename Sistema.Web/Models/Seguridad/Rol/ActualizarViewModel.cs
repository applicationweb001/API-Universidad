using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Rol
{
    public class ActualizarViewModel
    {
        [Required]
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "La descripción debe ser mayor a 10 caracteres y menor a 100")]
        public string descripcion { get; set; }
    }
}
