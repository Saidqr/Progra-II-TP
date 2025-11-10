using Medilink.Interfaces;
using Medilink.Models;
using Medilink.Context;
using Microsoft.EntityFrameworkCore;
namespace Medilink.Services
{
    public class ConsultaMedicaService : IConsultaMedicaService
    {
        private readonly MedilinkDbContext _context;
        public ConsultaMedicaService(MedilinkDbContext context)
        {
            _context = context;
        }
        public async Task<ConsultaMedica> AddConsulta(ConsultaMedica consulta)
        {
            consulta.Medico = await _context.Medicos.FindAsync(consulta.IdMedico);
            consulta.Paciente = await _context.Pacientes.FindAsync(consulta.IdPaciente);
            consulta.Receta = await _context.Recetas.FindAsync(consulta.IdReceta);
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task<bool> EliminarConsulta(int id)
        {
            var consulta = await GetConsulta(id);
            if (consulta == null) return false;

            _context.Remove(id);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<ConsultaMedica> GetConsulta(int id)
        {
            return await _context.Consultas.FindAsync(id);
        }

        public async Task<IEnumerable<ConsultaMedica>> GetConsultas()
        {
            return await _context.Consultas.ToListAsync();
        }

        public async Task<bool> UpdateConsulta(ConsultaMedica consulta)
        {
            var ConsultaExiste = await GetConsulta(consulta.Id);
            if (ConsultaExiste == null) return false;
            consulta.IdMedico = consulta.IdMedico;
            consulta.IdPaciente = consulta.IdPaciente;
            ConsultaExiste.Estado = consulta.Estado;
            ConsultaExiste.Observaciones = consulta.Observaciones;
            ConsultaExiste.Medico = consulta.Medico;
            //Añadir parte para Paciente
            ConsultaExiste.Fecha = consulta.Fecha;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Consultas.AnyAsync(m => m.Id == consulta.Id))
                    return false;
                else
                    throw;
            }
        }

      
    }
}