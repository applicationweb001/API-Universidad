using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace Sistema.Entidades.AdministracionAcademica
{
	public class Alumno
	{
		public int idAlumno { set; get; }
		public int idusuario { set; get; }
		public int idcarrera { set; get; }
		public int dni { set; get;}
		public DateTime fechanacimiento { set; get; }
		public string nombre { set; get; }
		public string direccion { set; get; }

		[ForeignKey("idusuario")]
		public Usuario usuario { set; get; }
		[ForeignKey("idcarrera")]
		public Carrera carrera { set; get; }
	}
}
