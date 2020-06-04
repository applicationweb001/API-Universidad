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
        [Required]
        public string nombre{ get; set; }
        [Required]
        public byte[] password { get; set; }
        [ForeignKey("idrol")]
        public Rol Rol { get; set; }
    }
}
