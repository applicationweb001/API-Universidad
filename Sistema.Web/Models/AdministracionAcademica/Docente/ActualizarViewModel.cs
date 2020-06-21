using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Docente
{
    public class ActualizarViewModel
    {
        [Required]
        public int iddocente { get; set; }
       
        public string correo { get; set; }

        public string dni { get; set; }

        [StringLength(100, MinimumLength = 5
            , ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 100")]
        public string nombre { get; set; }
        [StringLength(100, MinimumLength = 5
            , ErrorMessage = "El apellido debe ser mayor a 5 caracteres y menor a 100")]
        public string apellido { get; set; }



    }
}
