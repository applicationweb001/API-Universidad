using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Alumno
{
    public class AlumnoViewModel
    {
        public int idAlumno { set; get; }
        public string nombre { set; get; }
        public int dni {set;get;}
        public DateTime fechanacimiento {set;get;}
        public int idusuario {set;get;}
        public int idcarrera { set; get; }




    }
}
