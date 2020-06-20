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
        public int cantidad { get; set; }

        [Required]
        public int iddocente { get; set; }

        public string codigo_curso { get; set; }

    }
}
