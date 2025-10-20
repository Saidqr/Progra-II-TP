using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecetaController : ControllerBase
{
    private readonly IRecetaService _recetaService;
    public RecetaController(IRecetaService recetaService)
    {
        _recetaService = recetaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Receta>>> GetAll()
    {
        var recetas = await _recetaService.GetRecetas();
        return Ok(recetas);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Receta>> GetOneById(int id)
    {
        var receta = await _recetaService.GetReceta(id);
        if (receta == null) return NotFound();
        return Ok(receta);
    }
    [HttpPost]
    public async Task<ActionResult<Receta>> Create([FromBody] Receta receta)
    {
        var nuevaReceta = await _recetaService.AddReceta(receta);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevaReceta.Id }, nuevaReceta);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Receta>> CompleteUpdate([FromBody] Receta receta, int id)
    {
        if (id != receta.Id) return BadRequest();
        var resultadoReceta = await _recetaService.UpdateReceta(receta);
        if (!resultadoReceta) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var receta = await _recetaService.DeleteReceta(id);
        if (!receta) return NotFound($"No se encontro la receta con ID {id}");
        else return NoContent();
    }
    
}