using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet5.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet5.Repositories
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Cita> Citas { get; set; }

        public DbSet<Diagnostico> Diagnosticos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Medico>().ToTable("Medicos");


        }

    }
}
