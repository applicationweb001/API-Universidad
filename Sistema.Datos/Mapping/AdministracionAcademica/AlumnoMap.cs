using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.AdministracionAcademica
{
	public class AlumnoMap : IEntityTypeConfiguration<Alumno>
	{
		public void Configure(EntityTypeBuilder<Alumno> builder)
        {
			builder.ToTable("Alumno")
				.HasKey(cc => cc.idAlumno);
        }
	}
}
//WHYYY????? :(