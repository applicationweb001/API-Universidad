using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Sistema.Entidades.AdministracionAcademica;

namespace Sistema.Entidades.ProgramacionAcademica {
    public class Seccion {
        public int idseccion { get; set; }
        public int idcurso { get; set; }
        public int iddocente { get; set; }

        [ForeignKey ("idcurso")]
        public Curso Curso { get; set; }

        [ForeignKey ("iddocente")]
        public Docente Docente { get; set; }

        public int cantidad { get; set; }
        public ICollection<MatriculaSeccion> MatriculaSecciones { get; set; }

    }
}