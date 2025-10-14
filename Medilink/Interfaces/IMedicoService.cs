using System.Data;
using Medilink.Models;
namespace Medilink.Interfaces
{
    public interface IMedicoService
    {
        public List<Medico> GetMedicos();
        public Medico GetMedico(int id);
        public void AddMedico(Medico medico);
        public void UpdateMedico(Medico medico);
        public void DeleteMedico(int id);
    }
}