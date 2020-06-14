using Sistema.Entidades.ProgramacionAcademica;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.AdministracionAcademica
{
	public class MatriculaSeccion
	{
		public int idmatriculaSeccion { get; set; }
		public int idseccion { get; set; }
		public int idmatricula { get; set; }

		[ForeignKey("idmatricula")]
		public Matricula Matricula{ get; set; }
		[ForeignKey("idseccion")]
		public Seccion Seccion { get; set; }

	}
}
