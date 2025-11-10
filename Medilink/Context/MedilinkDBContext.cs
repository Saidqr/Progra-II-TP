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
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<RecetaMedicamento> RecetaMedicamentos { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
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
                            .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Persona>()
                            .WithMany()
                            .HasForeignKey("PersonaId")
                            .OnDelete(DeleteBehavior.Restrict),
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
            // ======== INSUMO (clase base con herencia TPH) =========
            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.ToTable("Insumos"); // ← TODAS las clases hijas van a esta tabla
                
                entity.HasKey(i => i.Id);
                
                entity.Property(i => i.Nombre).IsRequired();
                entity.Property(i => i.Codigo).HasMaxLength(50);
                entity.Property(i => i.Descripcion).IsRequired();
                entity.Property(i => i.cantidadInventario).IsRequired();
                
                // Configurar discriminador para herencia TPH
                entity.HasDiscriminator<string>("TipoInsumo")
                    .HasValue<Insumo>("Insumo")           // Tipo base (insumos genéricos)
                    .HasValue<Medicamento>("Medicamento"); // Tipo hijo
            });

            // ======== MEDICAMENTO =========
            modelBuilder.Entity<Medicamento>(entity =>
            {
                // NO uses ToTable() aquí, usa la tabla de Insumo
                
                entity.Property(m => m.FechaFabricacion)
                    .IsRequired()
                    .HasMaxLength(12);
                
                entity.Property(m => m.FechaVencimiento)
                    .IsRequired()
                    .HasMaxLength(12);
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

                entity.HasOne(c => c.Receta)
                    .WithOne()
                    .HasForeignKey<ConsultaMedica>(c => c.IdReceta)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //modelBuilder.Entity<ConsultaMedica>().HasKey(e => e.Id);
            //modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Medico).WithMany(m => m.Consultas).HasForeignKey(c => c.IdMedico).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<ConsultaMedica>().HasOne(c => c.Paciente).WithMany(m => m.Consultas).HasForeignKey(c => c.IdPaciente).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Estado).IsRequired();
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Observaciones).HasMaxLength(100);
            modelBuilder.Entity<ConsultaMedica>().Property(c => c.Fecha).IsRequired();

                        // ======== RECETA =========
            modelBuilder.Entity<Receta>(entity =>
            {
                entity.ToTable("Recetas");
                
                entity.HasKey(r => r.Id);
                
                entity.Property(r => r.Estado)
                    .IsRequired();
                
                entity.Property(r => r.FechaVencimiento)
                    .IsRequired();
                
                
                // Relación con RecetaMedicamentos
                entity.HasMany(r => r.RecetaMedicamentos)
                    .WithOne()
                    .HasForeignKey(rm => rm.IdReceta)
                    .OnDelete(DeleteBehavior.Cascade); // Si borras receta, borra sus medicamentos
            });

            // ======== RECETA MEDICAMENTO (tabla intermedia) =========
            modelBuilder.Entity<RecetaMedicamento>(entity =>
                {
                    entity.ToTable("RecetaMedicamentos");

                    entity.HasKey(rm => rm.Id);

                    entity.Property(rm => rm.Cantidad)
                        .IsRequired();
                    // Relación con Medicamento
                    entity.HasOne(rm => rm.medicamento)
                        .WithMany()
                        .HasForeignKey(rm => rm.IdMedicamento)
                        .OnDelete(DeleteBehavior.Restrict); // No borrar medicamento si está en recetas
                });

        }
    }
}
