using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Alumno
{
    public class CrearViewModel
    {
        public int idalumno { get; set; }
        [Required]
        public int idcarrera { set; get; }
        [Required]
        [StringLength(100, MinimumLength = 4,ErrorMessage ="El nombre debe ser mayor a 5 caracteres")]
        public string nombre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe ser mayor a 5 caracteres")]
        public string apellido { get; set; }
        [Required]
        [RegularExpression(pattern: "[+]?[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage = "Debe ingresar un DNI de 8 digitos")]
        public int dni { get; set; }
        [Required]
        public string direccion { set; get; }
        [Required]
        public DateTime fechaNacimiento { set; get; }
    }
}
