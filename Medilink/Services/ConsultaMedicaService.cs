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

            throw new NotImplementedException();
        }

        public async Task<bool> EliminarConsulta(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void RecetarMedicamentos(){}
    }
}