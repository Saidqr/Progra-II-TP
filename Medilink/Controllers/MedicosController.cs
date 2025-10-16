using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicosController : ControllerBase
{
    private readonly IMedicoService _medicoService;
    public MedicosController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medico>>> GetAll()
    {
        var medicos = await _medicoService.GetMedicos();
        return Ok(medicos);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Medico>> GetOneById(int id)
    {
        var medico = await _medicoService.GetMedico(id);
        if (medico == null) return NotFound();
        return Ok(medico);
    }
    [HttpPost]
    public async Task<ActionResult<Medico>> Create([FromBody] Medico medico)
    {
        var medicoNuevo = await _medicoService.AddMedico(medico);
        return CreatedAtAction(nameof(GetOneById), new { id = medicoNuevo.Id }, medicoNuevo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Medico>> CompleteUpdate([FromBody] Medico medico, int id)
    {
        if (id != medico.Id) return BadRequest();
        var resultadoMedico = await _medicoService.UpdateMedico(medico);
        if (!resultadoMedico) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var medico = await _medicoService.DeleteMedico(id);
        if (!medico) return NotFound($"No se encontro el medico con ID {id}");
        else return NoContent();
    }
    
}