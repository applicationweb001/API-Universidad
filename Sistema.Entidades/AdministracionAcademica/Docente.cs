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
        [ForeignKey("idusuario")]
        public Usuario usuario { get; set; }
        public int iddocente { get; set; }
       
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El apellido debe ser mayor a 10 caracteres y menor a 100")]
        public string apellido { get; set; }

        public string correo { get; set; }
        //odio a kevin caldito seas
        public string dni { get; set; }
       
    }
}
