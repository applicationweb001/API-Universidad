using Sistema.Web.Models.Seguridad.Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;



namespace Sistema.Web.Models.AdministracionAcademica.Docente
{
    public class CrearViewModel
    {
        [Required]
        public string nombre { get; set; }
        
        [Required]
        public string apellido { get; set; }
        
        [Required]
        public string correo { get; set; }

        [Required]
        public string dni { get; set; }

     
    }
}
