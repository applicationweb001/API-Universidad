using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Sistema.Entidades.AdministracionAcademica;
using System.Text;

namespace Sistema.Entidades.ProgramacionAcademica
{
    public class Seccion
    {
        public int idseccion { get; set; }
        public int idcurso { get; set; }
        [ForeignKey("iddocente")]
        public int iddocente { get; set; }

        [ForeignKey("idcurso")]
        public Curso Curso { get; set; }
<<<<<<< HEAD
        public Docente Docente { get; set; }

        public int cantidad { get; set; }

=======
        //[ForeignKey("iddocente")]
        //public Docente Docente{ get; set; }
        public ICollection<MatriculaSeccion> MatriculaSecciones { get; set; }
>>>>>>> 993dcf504995d7a3dc3507a2e1c992417add1959

    }
}
