using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.ProgramacionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.ProgramacionAcademica
{
    public class CursoCarreraMap : IEntityTypeConfiguration<CursoCarrera>
    {
        public void Configure(EntityTypeBuilder<CursoCarrera> builder)
        {
            builder.ToTable("cursoCarrera")
                .HasKey(cc => cc.idcursocarrera);

        }
    }
}
