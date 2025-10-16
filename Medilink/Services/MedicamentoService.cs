using Medilink.Interfaces;
using Medilink.Models;
using Medilink.Context;
using Microsoft.EntityFrameworkCore;

namespace Medilink.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly MedilinkDbContext _context;
        public MedicamentoService(MedilinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicamento>> GetMedicamentos()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamento> GetMedicamento(int id)
        {
            return await _context.Medicamentos.FindAsync(id);
        }

        public async Task<Medicamento> AddMedicamento(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
            return medicamento;
        }

        public async Task<bool> UpdateMedicamento(Medicamento medicamento)
        {
            _context.Entry(medicamento).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Medicamentos.AnyAsync(m => m.Id == medicamento.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteMedicamento(int id)
        {
            var medicamento = await GetMedicamento(id);
            if (medicamento == null) return false;
            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

