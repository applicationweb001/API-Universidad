using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.ProgramacionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.ProgramacionAcademica
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("curso")
                .HasKey(c => c.idcurso);
            builder.Property(c => c.nombre)
               .HasMaxLength(100);
           
        }
    }
}
