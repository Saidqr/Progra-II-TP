using Medilink.Models;
namespace Medilink.Interfaces
//Implementar Said
{
    public interface IMedicamentoService
    {
        Task<IEnumerable<Medicamento>> GetMedicamentos();
        Task<Medicamento> GetMedicamento(int id);
        Task<Medicamento> AddMedicamento(Medicamento medicamento);
        Task<bool> UpdateMedicamento(Medicamento medicamento);
        Task<bool> DeleteMedicamento(int id);
    }
}