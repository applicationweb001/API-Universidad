using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Curso
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        public string codigo_curso { get; set; }
        [Required]
        public List<int> Carreras { get; set; }

    }
}
