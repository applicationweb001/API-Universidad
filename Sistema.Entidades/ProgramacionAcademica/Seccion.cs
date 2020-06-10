using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.ProgramacionAcademica
{
    public class Seccion
    {
        public int idseccion { get; set; }
        public int idcurso { get; set; }
        //public int iddocente { get; set; }

        [ForeignKey("idcurso")]
        public Curso Curso { get; set; }
        //[ForeignKey("iddocente")]
        //public Docente Docente{ get; set; }


    }
}
