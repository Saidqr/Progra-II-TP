using Medilink.Models;
using Medilink.DTO;
namespace Medilink.Interfaces
{
    public interface IPersonaService
    {
        public Task<IEnumerable<Persona>> GetPersonasAsync();
        public Task<Persona> GetPersonaAsync(int id);
        public Task<Persona> GetByNombreUsuario(string nombreUsuario);
        public Task<Persona> AddPersonaAsync(RegistrarPersonaDto personaDto);
        public Task<bool> UpdatePersonaAsync(Persona persona);
        public Task<bool> DeletePersonaAsync(int id);
    }
}