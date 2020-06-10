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
       
        [StringLength(100, MinimumLength = 3
            , ErrorMessage = "El nombre debe ser mayor a 3 caracteres y menor a 100")]
        public string email { get; set; }

        public string dni { get; set; }

        //odio a kevin caldito seas

    }
}
