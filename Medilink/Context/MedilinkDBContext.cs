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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración con Fluent API
            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Matricula)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Especialidad).IsRequired();

                // Creamos un índice único para la matricula para evitar duplicados
                entity.HasIndex(e => e.Matricula).IsUnique();
            });

            modelBuilder.Entity<ConsultaMedica>().HasKey(e => e.Id);
            modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Medico).WithMany(m => m.Consultas).HasForeignKey(c => c.IdMedico).OnDelete(DeleteBehavior.Restrict);
            //Falta añadir paciente 
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Estado).IsRequired();
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Observaciones).HasMaxLength(100);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Fecha).IsRequired();


        }
    }
}