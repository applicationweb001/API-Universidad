using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Alumno
{
    //alumno
    public class CrearViewModel
    {
       
        [Required]
        [StringLength(100, MinimumLength =10,ErrorMessage ="El nombre debe ser mayor a 5 caracteres")]
        public string nombre { get; set; }
        [Required]
        [StringLength(8,MinimumLength =8,ErrorMessage ="Debe ser 8 caracters")]
        public int dni { set; get; }
        [Required]
        public string direccion { set; get; }
        [Required]
        public int idcarrera { set; get; }
        [Required]
        public int idusuario { set; get; }

    }   
}
