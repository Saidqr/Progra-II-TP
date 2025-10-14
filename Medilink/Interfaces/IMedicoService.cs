using System.Data;
using Medilink.Models;
namespace Medilink.Interfaces
{
    public interface IMedicoService
    {
        public Task<IEnumerable<Medico>> GetMedicos();
        public Task<Medico> GetMedico(int id);
        public Task<Medico> AddMedico(Medico medico);
        public Task<bool> UpdateMedico(Medico medico);
        public Task<bool> DeleteMedico(int id);
    }
}