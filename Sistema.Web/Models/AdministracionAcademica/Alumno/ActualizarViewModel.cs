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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe ser mayo de 3 caracteres")]
        public string nombre { set; get; }
        [Required]
        [StringLength(30,MinimumLength =3,ErrorMessage = "El apellido debe ser mayo de 3 caracteres")]
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
