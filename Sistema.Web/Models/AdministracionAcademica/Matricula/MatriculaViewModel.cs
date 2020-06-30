using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Matricula
{
    public class MatriculaViewModel
    {
   
        public int idmatricula { get; set; }
     
        public int idalumno { get; set; }
 
        public string anioacademico { get; set; }
        public int cursosalumno { get; internal set; }
        public int dni { get; internal set; }
        public string apellido { get; internal set; }
        public string nombre { get; internal set; }
        public string carrera { get; internal set; }
    }
}
