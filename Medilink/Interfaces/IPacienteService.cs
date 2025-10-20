using System.Data;
using Medilink.Models;
namespace Medilink.Interfaces
{
    public interface IPacienteService
    {
        public Task<IEnumerable<Paciente>> GetPacientes();
        public Task<Paciente> GetPaciente(int id);
        public Task<Paciente> AddPaciente(Paciente paciente);
        public Task<bool> UpdatePaciente(Paciente paciente);
        public Task<bool> DeletePaciente(int id);
    }
}