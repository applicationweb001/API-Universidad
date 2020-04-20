using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.RegistrosAc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.RegistrosAc
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
