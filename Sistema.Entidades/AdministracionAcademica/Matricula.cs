﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.AdministracionAcademica
{
    public class Matricula
    {
        public int idmatricula { get; set; }
        public int idalumno { get; set; }
        public int anioacademico { get; set; }
        
        [ForeignKey("idalumno")]
        public Alumno Alumno { get; set; }
       
    }
}

