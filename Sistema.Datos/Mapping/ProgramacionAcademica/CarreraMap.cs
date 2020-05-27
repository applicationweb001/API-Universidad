using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.ProgramacionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.ProgramacionAcademica
{
    public class CarreraMap : IEntityTypeConfiguration<Carrera>
    {
        public void Configure(EntityTypeBuilder<Carrera> builder)
        {
            builder.ToTable("carrera")
                .HasKey(c => c.idcarrera);

            builder.Property(c => c.nombre)
                .HasMaxLength(100);
        }

    }
}
