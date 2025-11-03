using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Medilink.Controllers;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _pacienteService;
    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetAll()
    {
        var pacientes = await _pacienteService.GetPacientes();
        return Ok(pacientes);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Paciente>> GetOneById(int id)
    {
        var paciente = await _pacienteService.GetPaciente(id);
        if (paciente == null) return NotFound();
        return Ok(paciente);
    }
    [HttpPost]
    public async Task<ActionResult<Paciente>> Create([FromBody] Paciente paciente)
    {
        var pacienteNuevo = await _pacienteService.AddPaciente(paciente);
        return CreatedAtAction(nameof(GetOneById), new { id = pacienteNuevo.Id }, pacienteNuevo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Paciente>> CompleteUpdate([FromBody] Paciente paciente, int id)
    {
        if (id != paciente.Id) return BadRequest();
        var resultadoPaciente = await _pacienteService.UpdatePaciente(paciente);
        if (!resultadoPaciente) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var paciente = await _pacienteService.DeletePaciente(id);
        if (!paciente) return NotFound($"No se encontro el paciente con ID {id}");
        else return NoContent();
    }
    
}