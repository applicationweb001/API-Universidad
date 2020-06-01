using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Seguridad
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol")
                .HasKey(c => c.idrol);
            builder.Property(c => c.nombre)
                .HasMaxLength(100);
        }

    }
}
