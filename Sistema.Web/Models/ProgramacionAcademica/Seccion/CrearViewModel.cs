using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Seccion
{
    public class CrearViewModel
    {
        [Required]
        public int idcurso { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "El nombre debe ser mayor a 1 caracteres y menor a 2")]
        public int cantidad { get; set; }

        [Required]
        public int iddocente { get; set; }


    }
}
