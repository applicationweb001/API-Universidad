using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.AdministracionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.AdministracionAcademica
{
    public class MatriculaSeccionMap : IEntityTypeConfiguration<MatriculaSeccion>
    {
        public void Configure(EntityTypeBuilder<MatriculaSeccion> builder)
        {
            builder.ToTable("matriculaSeccion").
                HasKey(cc => cc.idmatriculaSeccion);
        }
    }
}
