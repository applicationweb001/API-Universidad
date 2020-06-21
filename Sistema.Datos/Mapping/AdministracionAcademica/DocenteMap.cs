using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.AdministracionAcademica
{
    public class DocenteMap : IEntityTypeConfiguration<Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.ToTable("docente")
                .HasKey(c => c.iddocente);

         /*   builder.Property(c => c.nombre)
                .HasMaxLength(100);*/
        }
    }
}
