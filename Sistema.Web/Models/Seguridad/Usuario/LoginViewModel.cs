using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class LoginViewModel
    {
        [Required]
  
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
