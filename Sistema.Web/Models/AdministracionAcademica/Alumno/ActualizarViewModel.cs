using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Alumno
{
    public class ActualizarViewModel
    {
        [Required]
        public int idAlumno { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "el nombre debe ser mayor ")]
        public string nombre { set; get; }
        [Required]
        [StringLength(8,MinimumLength =8,ErrorMessage ="El dni debe ser 8 caracteres")]
        public string apellido { set; get; }
        public int dni {set; get;}
        [Required]
        public DateTime fechanacimiento {set;get;}
        [Required]
        public int idusuario {get; set; }
        [Required]
        public int idcarrera {get;set;}

        public string direccion { get; set; }




        

    }
}
