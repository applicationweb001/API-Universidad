using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Seccion
{
    public class SelectViewModel
    {
        public int idseccion { get; set; }
        public int idcurso { get; set; }
        public string nombreCurso { get; set; }
        public string nombreDocente { get; set; }
        public string codigo_seccion { get; set; }
        public int alumnosDisponibles { get; internal set; }
        public int totalCupos { get; internal set; }
    }
}
