using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.AdministracionAcademica
{
    public class Docente
    {
        public int iddocente { get; set; }
       
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 100")]
        public string nombre { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El apellido debe ser mayor a 5 caracteres y menor a 100")]
        public string apellido { get; set; }
        [Required]
        [EmailAddress]
        public string correo { get; set; }
        public int dni { get; set; }
        public ICollection<Seccion> Secciones { get; set; }

    }
}
