using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.ProgramacionAcademica.Curso
{
    public class CursoViewModel
    {
        public int idcurso { get; set; }
        public string nombre { get; set; }
        public bool condicion { get; set; }
        public int carreras { get; set; }

    }
}
