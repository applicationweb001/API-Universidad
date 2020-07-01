using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Seccion
{
    public class ActualizarViewModel
    {
        [Required]
        public int idseccion { get; set; }
        [Required]
        public int idcurso { get; set; }
        [Required]
        public int iddocente { get; set; }

        [Required]
        public int cantidad { get; set; }


    }
}
