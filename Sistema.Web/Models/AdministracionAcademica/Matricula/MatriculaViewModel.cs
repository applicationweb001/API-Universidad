using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Matricula
{
    public class MatriculaViewModel
    {
        [Required]
        public int idmatricula { get; set; }
        [Required]
        public int idalumno { get; set; }
        [Required]
        public int anioacademico { get; set; }
    }
}
