using System.Runtime.Versioning;
using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MedicamentoController : ControllerBase
{
    private readonly IMedicamentoService _medicamentoService;

    public int Id { get; private set; }

    public MedicamentoController(IMedicamentoService medicamentoService)
    {
        _medicamentoService = medicamentoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medicamento>>> GetAll()
    {
        var medicamentos = await _medicamentoService.GetMedicamentos();
        return Ok(medicamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Medicamento>> GetOneById(int id)
    {
        var medicamento = await _medicamentoService.GetMedicamento(id);
        if (medicamento == null) return NotFound();
        return Ok(medicamento);
    }

    [HttpPost]
    public async Task<ActionResult<Medicamento>> Create([FromBody] Medicamento medicamento)
    {
        var nuevoMedicamento = await _medicamentoService.AddMedicamento(medicamento);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevoMedicamento.Id }, nuevoMedicamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CompleteUpdate([FromBody] Medicamento medicamento, int id)
    {
        if (id != medicamento.Id) return BadRequest();
        var resultado = await _medicamentoService.UpdateMedicamento(medicamento);
        if (!resultado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var medicamento = await _medicamentoService.DeleteMedicamento(id);
        if (!medicamento) return NotFound($"No se encontró el medicamento con ID {id}");
        else return NoContent();
    }
}