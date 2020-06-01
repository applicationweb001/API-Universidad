using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Seguridad
{
    public class Usuario
    {
        public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe ser mayor a 3 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        public byte[] password { get; set; }
        [ForeignKey("idrol")]
        public Rol Rol { get; set; }
    }
}
