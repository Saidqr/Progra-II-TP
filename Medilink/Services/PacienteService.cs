using Medilink.Interfaces;
using Medilink.Models;
using Medilink.Context;
using Microsoft.EntityFrameworkCore;
namespace Medilink.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly MedilinkDbContext _context;
        public PacienteService(MedilinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> GetPaciente(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }
        public async Task<Paciente> AddPaciente(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<bool> UpdatePaciente(Paciente paciente)
        {
            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pacientes.AnyAsync(m => m.Id == paciente.Id))
                    return false;
                else
                    throw;
            }
        }
        public async Task<bool> DeletePaciente(int id)
        {
            var paciente = await GetPaciente(id);
            if (paciente == null) return false;

            _context.Remove(id);
            _context.SaveChangesAsync();
            return true;
        }
    }
}