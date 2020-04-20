using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.RegistrosAc;
using Sistema.Entidades.RegistrosAc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DBContextSistema : DbContext
    {
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoCarrera> CursoCarreras { get; set; }


        public DBContextSistema(DbContextOptions<DBContextSistema> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarreraMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new CursoCarreraMap());

        }

    }
}
