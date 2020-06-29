using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class ActualizarViewModel
    {
        public int idalumno { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        public bool act_password { get; set; }
    }
}
