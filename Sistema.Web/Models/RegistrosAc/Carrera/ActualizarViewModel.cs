using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.RegistrosAc.Carrera
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcarrera { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        public bool condicion { get; set; }
    }
}
