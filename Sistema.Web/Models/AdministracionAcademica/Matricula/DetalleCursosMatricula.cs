using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.AdministracionAcademica.Matricula
{
    public class DetalleCursosMatricula
    {
        public int idseccion { get; set; }
        public string codigo_seccion { get; set; }
        public int alumnosRegistrados { get; set; }
        public string nombreDocente { get;  set; }
        public string nombreCurso { get; set; }
    }
}
