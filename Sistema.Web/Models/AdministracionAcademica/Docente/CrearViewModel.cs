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
        [EmailAddress]
        public string correo { get; set; }
        [RegularExpression(pattern: "[+]?[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage ="Debe ingresar un DNI de 8 digitos")]
        public int dni { get; set; }

    }
}
