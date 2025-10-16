using Medilink.Interfaces;
using Medilink.Models;
using Medilink.Context;
using Microsoft.EntityFrameworkCore;
namespace Medilink.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly MedilinkDbContext _context;
        public MedicoService(MedilinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medico>> GetMedicos()
        {
            return await _context.Medicos.ToListAsync();
        }

        public async Task<Medico> GetMedico(int id)
        {
            return await _context.Medicos.FindAsync(id);
        }
        public async Task<Medico> AddMedico(Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChangesAsync();
            return medico;
        }
        public async Task<bool> UpdateMedico(Medico medico)
        {
            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Medicos.AnyAsync(m => m.Id == medico.Id))
                    return false;
                else
                    throw;
            }
        }
        public async Task<bool> DeleteMedico(int id)
        {
            var medico = await GetMedico(id);
            if (medico == null) return false;

            _context.Remove(id);
            _context.SaveChangesAsync();
            return true;
        }
    }
}