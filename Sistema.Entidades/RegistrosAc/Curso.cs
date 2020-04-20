using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.RegistrosAc
{
    public class Curso
    {
        public int idcurso { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "La descripcion debe ser mayor a 10 caracteres y menor a 200")]
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public ICollection<CursoCarrera> CursoCarreras { get; set; }
    }
}
