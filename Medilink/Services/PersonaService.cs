using Medilink.Context;
using Medilink.Interfaces;
using Medilink.Models;
using Microsoft.EntityFrameworkCore;
using Medilink.DTO;
namespace Medilink.Services;

public class PersonaService : IPersonaService
{
    private readonly MedilinkDbContext _dbContext;
    public PersonaService(MedilinkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Persona> AddPersonaAsync(RegistrarPersonaDto dto)
    {
        var passHash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasenia);

        // Buscar los roles que coincidan con los IDs recibidos
        var roles = await _dbContext.Roles.Where(r => dto.IdRoles.Contains(r.Id)).ToListAsync();
        //Esto esta bien ?? Pq no dejaria que se inserte ninguna persona si no tenia un rol creado previamenteÇ
        if (!roles.Any())
            return null;

        var persona = new Persona
        {
        Nombre = dto.Nombre,
        NombreUsuario = dto.NombreUsuario,
        PassHash = passHash,
        Apellido = dto.Apellido,
        fechaNacimiento = dto.fechaNacimiento,
        DNI = dto.DNI,
        Roles = roles
        };

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
        return _dbContext.Personas.Include(p => p.Roles).FirstOrDefault(p => p.NombreUsuario == nombreUsuario);
    }

    public async Task<Persona> GetPersonaAsync(int id)
    {
        return await _dbContext.Personas.Include(p => p.Roles).FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Persona>> GetPersonasAsync()
    {
        return await _dbContext.Personas.Include(p => p.Roles).ToListAsync();
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