using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.AdministracionAcademica;


namespace Sistema.Datos.Mapping.AdministracionAcademica
{
	public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
		public void Configure(EntityTypeBuilder<Matricula> builder)
		{
			builder.ToTable("Matricula")
				.HasKey(c => c.idmatricula);
		}
    }
}