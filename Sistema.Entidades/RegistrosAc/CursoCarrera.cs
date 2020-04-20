using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.RegistrosAc
{
    public class CursoCarrera
    {
        public int idcursocarrera { get; set; }
        public int idcurso { get; set; }
        public int idcarrera { get; set; }

        [ForeignKey("idcarrera")]
        public Carrera Carrera { get; set; }
        [ForeignKey("idcurso")]
        public Curso Curso { get; set; }
    }
}
