using System.Data;
using Medilink.Models;
namespace Medilink.Interfaces
{
    public interface IRecetaService
    {
        public Task<IEnumerable<Receta>> GetRecetas();
        public Task<Receta> GetReceta(int id);
        public Task<Receta> AddReceta(Receta receta);
        public Task<bool> UpdateReceta(Receta receta);
        public Task<bool> DeleteReceta(int id);
    }
}