using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Seccion
{
    public class SeccionViewModel
    {
        public int idseccion { get; set; }
        public int idcurso { get; set; }
        public string nombrecurso { get; set; }
        public int cantidad { get; set; }
        public int iddocente { get; set; }
        public string nombredocente { get; set; }
        public string codigo_seccion { get; set; }



    }
}
