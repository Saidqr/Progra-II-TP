using Medilink.Context;
using Medilink.Interfaces;
using Medilink.Models;
using Microsoft.EntityFrameworkCore;
namespace Medilink.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly MedilinkDbContext _dbContext;
        public RecetaService(MedilinkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Receta>> GetRecetas()
        {
            return await _dbContext.Recetas.ToListAsync();
        }
        public async Task<Receta> GetReceta(int id)
        {
            return await _dbContext.Recetas.FindAsync(id);
        }

        public async Task<Receta> AddReceta(Receta receta, int idConsulta)
        {
            var consulta = await _dbContext.Consultas.FindAsync(idConsulta);

            if (consulta == null) throw new InvalidOperationException($"No se encontró una consulta médica con ID {idConsulta}.");

            await _dbContext.Recetas.AddAsync(receta);
            await _dbContext.SaveChangesAsync();
            return receta;
        }

        public async Task<bool> UpdateReceta(Receta receta)
        {
            var recetaModificada = await GetReceta(receta.Id);
            if (recetaModificada == null) return false;

            //Falta ver bien la parte de los medicamentos dentro de la receta
            recetaModificada.Estado = receta.Estado;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReceta(int id)
        {
            var recetaAeliminar = await GetReceta(id);
            if (recetaAeliminar == null) return false;
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}