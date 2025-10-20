using Medilink.Controllers;
using Medilink.Models;
namespace Medilink.Interfaces
{
    public interface IMedicamentoService
    {
        Task<IEnumerable<Medicamento>> GetMedicamentos();
        Task<Medicamento> GetMedicamento(int id);
        Task<Medicamento> AddMedicamento(Medicamento medicamento);
        Task<bool> UpdateMedicamento(Medicamento medicamento);
        Task<bool> DeleteMedicamento(int id);
        Task<bool> UpdateMedicamento(MedicamentoController medicamento);
    }
}