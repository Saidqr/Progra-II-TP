using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultaMedicaController : ControllerBase
{
    private readonly IConsultaMedicaService _consultaService;
    public ConsultaMedicaController(IConsultaMedicaService consultaService)
    {
        _consultaService = consultaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConsultaMedica>>> GetAll()
    {
        var consultas = await _consultaService.GetConsultas();
        return Ok(consultas);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ConsultaMedica>> GetOneById(int id)
    {
        var consulta = await _consultaService.GetConsulta(id);
        if (consulta == null) return NotFound();
        return Ok(consulta);
    }
    [HttpPost]
    public async Task<ActionResult<ConsultaMedica>> Create([FromBody] ConsultaMedica consulta)
    {
        var nuevaConsulta = await _consultaService.AddConsulta(consulta);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevaConsulta.Id }, nuevaConsulta);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ConsultaMedica>> CompleteUpdate([FromBody] ConsultaMedica consulta, int id)
    {
        if (id != consulta.Id) return BadRequest();
        var resultadoConsulta = await _consultaService.UpdateConsulta(consulta);
        if (!resultadoConsulta) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var consulta = await _consultaService.EliminarConsulta(id);
        if (!consulta) return NotFound($"No se encontro la consulta con ID {id}");
        else return NoContent();
    }
    
}