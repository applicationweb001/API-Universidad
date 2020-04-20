using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.RegistrosAc.Curso
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string nombre { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "El nombre debe ser mayor a 10 caracteres y menor a 100")]
        public string descripcion { get; set; }
        public List<CursoCarreraViewModel> cursoCarreras { get; set;}

    }
}
