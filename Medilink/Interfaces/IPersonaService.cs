using Medilink.Models;

namespace Medilink.Interfaces
{
    public interface IPersonaService
    {
        public Task<IEnumerable<Persona>> GetPersonasAsync();
        public Task<Persona> GetPersonaAsync(int id);
        public Task<Persona> GetByNombreUsuario(string nombreUsuario);
        public Task<Persona> AddPersonaAsync(Persona persona);
        public Task<bool> UpdatePersonaAsync(Persona persona);
        public Task<bool> DeletePersonaAsync(int id);
    }
}