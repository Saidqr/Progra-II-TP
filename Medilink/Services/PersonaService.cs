using Medilink.Context;
using Medilink.Interfaces;
using Medilink.Models;
using Microsoft.EntityFrameworkCore;
namespace Medilink.Services;

public class PersonaService : IPersonaService
{
    private readonly MedilinkDbContext _dbContext;
    public PersonaService(MedilinkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Persona> AddPersonaAsync(Persona persona)
    {
            await _dbContext.Personas.AddAsync(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
    }

    public async Task<bool> DeletePersonaAsync(int id)
    {
            var personaAeliminar = await GetPersonaAsync(id);
            if (personaAeliminar == null) return false;

            _dbContext.Personas.Remove(personaAeliminar);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<Persona> GetByNombreUsuario(string nombreUsuario)
    {
        return _dbContext.Personas.FirstOrDefault(p => p.NombreUsuario == nombreUsuario);
    }

    public async Task<Persona> GetPersonaAsync(int id)
    {
        return await _dbContext.Personas.FindAsync(id);
    }

    public async Task<IEnumerable<Persona>> GetPersonasAsync()
    {
        return await _dbContext.Personas.ToListAsync();
    }

    public async Task<bool> UpdatePersonaAsync(Persona persona)
    {
        var personaModificada = await GetPersonaAsync(persona.Id);
            if (personaModificada == null) return false;

            personaModificada.Nombre = persona.Nombre;
            personaModificada.Apellido = persona.Apellido;
            personaModificada.NombreUsuario = persona.NombreUsuario;
            personaModificada.fechaNacimiento = persona.fechaNacimiento;
            personaModificada.DNI = persona.DNI;   
            await _dbContext.SaveChangesAsync();
            return true;
    }
}