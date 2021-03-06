﻿using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.AdministracionAcademica;
using Sistema.Datos.Mapping.ProgramacionAcademica;
using Sistema.Datos.Mapping.Seguridad;
using Sistema.Entidades.AdministracionAcademica;
using Sistema.Entidades.ProgramacionAcademica;
using Sistema.Entidades.Seguridad;
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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<MatriculaSeccion> MatriculaSecciones { get; set; } 


        public DBContextSistema(DbContextOptions<DBContextSistema> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarreraMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new CursoCarreraMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AlumnoMap());
            modelBuilder.ApplyConfiguration(new MatriculaMap());
            modelBuilder.ApplyConfiguration(new SeccionMap());
            modelBuilder.ApplyConfiguration(new DocenteMap());
            modelBuilder.ApplyConfiguration(new MatriculaSeccionMap());
        }

        

    }
}
