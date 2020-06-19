using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.ProgramacionAcademica
{
    public class Curso
    {
        public int idcurso { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        public string codigo_curso { get; set; }
        public bool condicion { get; set; }
        public ICollection<CursoCarrera> CursoCarreras { get; set; }
    }
}
