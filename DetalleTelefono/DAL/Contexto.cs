﻿using DetalleTelefono.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DetalleTelefono.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> personas  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source = DetelleTelefono.db");
        }
    }
}
