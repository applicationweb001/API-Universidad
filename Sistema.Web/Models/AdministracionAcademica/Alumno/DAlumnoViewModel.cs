using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Alumno
{
    public class DAlumnoViewModel
    {
        public int idAlumno { get; set; }
        public int idusuario { get; set; }
        public int idcarrera { get; set; }
        public string nombre { get; set; }
        public int dni { get; set; }
        public string direccion { get; set; }
        public DateTime fechanacimiento { get; set; }




    }
}
