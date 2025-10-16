using Medilink.Models;
using Microsoft.EntityFrameworkCore;

namespace Medilink.Context
{

    public class MedilinkDbContext : DbContext
    {
        public MedilinkDbContext(DbContextOptions<MedilinkDbContext> options) : base(options)
        {
        }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<ConsultaMedica> Consultas { get; set; }
        public DbSet<Hospital> Hospitales { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Rol).WithMany().HasForeignKey(p => p.IdRol).OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Rol>().ToTable("Roles");


            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medicos");

                entity.Property(e => e.Matricula).HasMaxLength(20).IsRequired();

                entity.Property(e => e.Especialidad).IsRequired();

                entity.HasIndex(e => e.Matricula).IsUnique();
            });

            modelBuilder.Entity<ConsultaMedica>().HasKey(e => e.Id);
            modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Medico).WithMany(m => m.Consultas).HasForeignKey(c => c.IdMedico).OnDelete(DeleteBehavior.Restrict);
            //Falta añadir paciente 
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Estado).IsRequired();
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Observaciones).HasMaxLength(100);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Fecha).IsRequired();

            modelBuilder.Entity<Hospital>().HasKey(h => h.Id);
            modelBuilder.Entity<Hospital>().Property(h => h.Nombre).IsRequired();
            modelBuilder.Entity<Hospital>().HasMany(h => h.Medicos).WithMany(h => h.Hospitales);

            modelBuilder.Entity<Medicamento>().HasKey(m => m.Id);
            modelBuilder.Entity<Medicamento>().Property(m => m.Nombre).IsRequired();
            modelBuilder.Entity<Medicamento>().Property(m => m.Descripcion).IsRequired();
            modelBuilder.Entity<Medicamento>().Property(m => m.FechaFabricacion).IsRequired().HasMaxLength(12);
            modelBuilder.Entity<Medicamento>().Property(m => m.FechaVencimiento).IsRequired().HasMaxLength(12);
        

        }
    }
}