using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Seguridad
{
    public class Rol
    {
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 30, ErrorMessage = "La descricpión debe ser mayor a 20 caracteres y menor a 100")]
        public string descripcion { get; set; }

    }
}
