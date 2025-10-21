using System.Reflection.Metadata;
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
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ConsultaMedica> Consultas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Receta> Recetas{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Apellido).IsRequired();
                entity.Property(p => p.DNI).IsRequired();
                entity.Property(p => p.fechaNacimiento).IsRequired();

                entity.HasMany(p => p.Roles).WithMany().UsingEntity<Dictionary<string, object>>("PersonaRol", j => j.HasOne<Rol>().WithMany().HasForeignKey("RolId").OnDelete(DeleteBehavior.Restrict), j => j.HasOne<Persona>().WithMany().HasForeignKey("PersonaId").OnDelete(DeleteBehavior.Restrict));
            });


            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medicos");

                entity.Property(e => e.Matricula).HasMaxLength(20).IsRequired();

                entity.Property(e => e.Especialidad).IsRequired();

                entity.HasIndex(e => e.Matricula).IsUnique();
            });
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Pacientes");
                entity.Property(p => p.Expediente).IsRequired();
            });

            modelBuilder.Entity<ConsultaMedica>().HasKey(e => e.Id);
            modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Medico).WithMany(m => m.Consultas).HasForeignKey(c => c.IdMedico).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Paciente).WithMany(m => m.Consultas).HasForeignKey(c => c.IdPaciente).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Estado).IsRequired();
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Observaciones).HasMaxLength(100);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Fecha).IsRequired();

            modelBuilder.Entity<Medicamento>().HasKey(m => m.Id);
            modelBuilder.Entity<Medicamento>().Property(m => m.Nombre).IsRequired();
            modelBuilder.Entity<Medicamento>().Property(m => m.Descripcion).IsRequired();
            modelBuilder.Entity<Medicamento>().Property(m => m.FechaFabricacion).IsRequired().HasMaxLength(12);
            modelBuilder.Entity<Medicamento>().Property(m => m.FechaVencimiento).IsRequired().HasMaxLength(12);


        }
    }
}