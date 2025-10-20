using Medilink.Models;
namespace Medilink.Interfaces
//Implementar Said
{
    public interface IInsumoService
    {
        Task<IEnumerable<Insumo>> GetInsumos();
        Task<Insumo> GetInsumo(int id);
        Task<Insumo> AddInsumo(Insumo insumo);
        Task<bool> UpdateInsumo(Insumo insumo);
        Task<bool> DeleteInsumo(int id);
    }
}


