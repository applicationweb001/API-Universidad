using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.RegistrosAc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.RegistrosAc
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario")
                .HasKey(c => c.idusuario);

            builder.Property(c => c.nombre)
                .HasMaxLength(100);
            builder.Property(c => c.contrasenia)
               .HasMaxLength(12);

        }

    }
}
