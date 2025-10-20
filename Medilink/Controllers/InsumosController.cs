using System.Runtime.Versioning;
using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]

public class InsumoController : ControllerBase
{
    private readonly IInsumoService _insumoService;
    public InsumoController(IInsumoService insumoService)
    {
        _insumoService = insumoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Insumo>>> GetAll()
    {
        var insumos = await _insumoService.GetInsumos();
        return Ok(insumos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Insumo>> GetOneById(int id)
    {
        var insumo = await _insumoService.GetInsumo(id);
        if (insumo == null) return NotFound();
        return Ok(insumo);
    }

    [HttpPost]
    public async Task<ActionResult<Insumo>> Create([FromBody] Insumo insumo)
    {
        var nuevoInsumo = await _insumoService.AddInsumo(insumo);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevoInsumo.Id }, nuevoInsumo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CompleteUpdate([FromBody] Insumo insumo, int id)
    {
        if (id != insumo.Id) return BadRequest();
        var resultado = await _insumoService.UpdateInsumo(insumo);
        if (!resultado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var insumo = await _insumoService.DeleteInsumo(id);
        if (!insumo) return NotFound($"No se encontró el insumo con ID {id}");
        else return NoContent();
    }
}