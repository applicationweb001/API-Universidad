using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.ProgramacionAcademica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.ProgramacionAcademica
{
    public class SeccionMap : IEntityTypeConfiguration<Seccion>
    {       
        public void Configure(EntityTypeBuilder<Seccion> builder)
        {
             builder.ToTable("seccion")
                .HasKey(c => c.idseccion);
        }
               

    }
}
