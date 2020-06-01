using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Seguridad.Usuario
{
    public class UsuarioViewModel
    {
        public int idusuario { get; set; }
        public int idrol { get; set; }
        public string rol { get; set; }
        public string nombre { get; set; }
        public byte[] password { get; set; }

    }
}
