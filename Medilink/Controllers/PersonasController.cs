using System.Security;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Medilink.Interfaces;
using Medilink.Models;
using Medilink.DTO;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly IPersonaService _personaService;

    public PersonasController(IPersonaService personaService)
    {
        _personaService = personaService;
    }

    [HttpGet]
    [Authorize(Roles = "ApiUser,Admin")] // Permite tanto usuarios API como admins
    public async Task<ActionResult<List<PersonaDto>>> GetAll()
    {
        //Imprimir todas las claims en un for each
        foreach (var claim in User.Claims)
        {
            Console.WriteLine(claim.Type + ": " + claim.Value);
        }

        var personas = await _personaService.GetPersonasAsync();
        var personasDto = personas.Select(p => new PersonaDto(p.Nombre, p.NombreUsuario, p.Apellido, p.fechaNacimiento, p.DNI)).ToList();
        return Ok(personasDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaDto>> GetByID(int id)
    {
        var p = await _personaService.GetPersonaAsync(id);
        if (p == null) return NotFound($"No se encontro la persona con ID {id}");
        return Ok(p);
    }

    [HttpPost]
    public async Task<ActionResult<PersonaDto>> Registrar([FromBody] RegistrarPersonaDto personaDto)
    {
        var passHash = BCrypt.Net.BCrypt.HashPassword(personaDto.Contrasenia);
        var persona = new Persona
        {
            Nombre = personaDto.Nombre,
            NombreUsuario = personaDto.NombreUsuario,
            PassHash = passHash,
            Apellido = personaDto.Apellido,
            fechaNacimiento=personaDto.fechaNacimiento,
            DNI = personaDto.DNI,
        };
        persona = await _personaService.AddPersonaAsync(persona);
        return CreatedAtAction(nameof(GetByID), new { id = persona.Id }, new PersonaDto(persona.Nombre, persona.NombreUsuario, persona.Apellido, persona.fechaNacimiento, persona.DNI));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personaService.DeletePersonaAsync(id);
        
        if (result) return NoContent();
        else return NotFound($"No se encontro la persona con ID {id}");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Persona>> Actualizar([FromBody] Persona persona, int id)
    {
        var person = GetByID(id);
        if (person == null) return NotFound($"No se encontro la persona con ID {id}");
        var p = await _personaService.UpdatePersonaAsync(persona);
        return Ok(p);
    }
}