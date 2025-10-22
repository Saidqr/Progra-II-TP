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

            /*
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
            });*/
            modelBuilder.Entity<Persona>(entity =>
    {
        entity.ToTable("Personas");

        entity.HasKey(p => p.Id);

        entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);

        entity.Property(p => p.Apellido).IsRequired().HasMaxLength(100);

        entity.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(50);

        entity.Property(p => p.PassHash)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(p => p.DNI)
              .IsRequired()
              .HasMaxLength(20);

        entity.Property(p => p.fechaNacimiento)
              .IsRequired();

        // Relación muchos a muchos con Rol
        entity.HasMany(p => p.Roles)
              .WithMany()
              .UsingEntity<Dictionary<string, object>>(
                  "PersonaRol",j => j.HasOne<Rol>()
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade),
                  j => j.HasOne<Persona>()
                        .WithMany()
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade),
                  j =>
                  {
                      j.HasKey("PersonaId", "RolId");
                      j.ToTable("PersonasRoles");
                  });
    });

    // ======== ROL (herencia TPH) =========
    modelBuilder.Entity<Rol>(entity =>
    {
        entity.ToTable("Roles");

        entity.HasKey(r => r.Id);

        // Definimos discriminador para herencia
        entity.HasDiscriminator<string>("TipoRol")
              .HasValue<Medico>("Medico")
              .HasValue<Paciente>("Paciente");
    });

    // ======== MÉDICO =========
    modelBuilder.Entity<Medico>(entity =>
    {
        entity.Property(m => m.Matricula)
              .HasMaxLength(50);

        entity.Property(m => m.Especialidad)
              .HasMaxLength(100);
    });

    // ======== PACIENTE =========
    modelBuilder.Entity<Paciente>(entity =>
    {
        entity.Property(p => p.Expediente)
              .HasMaxLength(100);
    });

    // ======== CONSULTA MÉDICA =========
    modelBuilder.Entity<ConsultaMedica>(entity =>
    {
        entity.ToTable("ConsultasMedicas");

        entity.HasKey(c => c.Id);

        entity.HasOne(c => c.Medico)
              .WithMany(m => m.Consultas)
              .HasForeignKey(c => c.IdMedico)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.Paciente)
              .WithMany(p => p.Consultas)
              .HasForeignKey(c => c.IdPaciente)
              .OnDelete(DeleteBehavior.Restrict);
    });

            //modelBuilder.Entity<ConsultaMedica>().HasKey(e => e.Id);
            //modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Medico).WithMany(m => m.Consultas).HasForeignKey(c => c.IdMedico).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Paciente).WithMany(m => m.Consultas).HasForeignKey(c => c.IdPaciente).OnDelete(DeleteBehavior.Restrict);
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