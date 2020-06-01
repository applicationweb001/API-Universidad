using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class UsuarioViewModel
    {
        public int idusuario { get; set; }
        public string nombre { get; set; }
        public string contrasenia { get; set; }
        public int roles { get; set; }

    }
}
