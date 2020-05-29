using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.RegistrosAc
{
    public class Usuario
    {
        public int idusuario { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "La contraseña debe ser mayor a 6 caracteres y menor a 12")]
        public string contrasenia { get; set; }
        [ForeignKey("idrol")]
        public Rol rol { get; set; }
    }
}
