using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
using Medilink.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace Medilink.Controllers;


[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class RecetaController : ControllerBase
{
    
    private readonly IRecetaService _recetaService;
    private readonly FirmaDigitalService _firmaService;
    public RecetaController(IRecetaService recetaService, FirmaDigitalService firmaService  )
    {
        _recetaService = recetaService;
        _firmaService = firmaService;
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
public async Task<ActionResult<Receta>> Create([FromBody] Receta receta, int idConsulta)
{
    // Validar que recetaMedicamentos no sea null ni vacío
    if (receta.RecetaMedicamentos == null || receta.RecetaMedicamentos.Count == 0)
    {
        return BadRequest(new { message = "Debe enviar al menos un medicamento en la receta." });
    }

    // Opcional: limpiar ids que no deben enviarse para evitar problemas
    foreach (var rm in receta.RecetaMedicamentos)
    {
        rm.Id = 0; // Si tienes Id en RecetaMedicamento que es Identity, ponerlo en 0 para que EF genere uno nuevo
        rm.IdReceta = 0; // Igual, dejar en 0
        rm.Medicamento = null; // Evitar insertar medicamento completo
    }

    try
    {
        var nuevaReceta = await _recetaService.AddReceta(receta, idConsulta);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevaReceta.Id }, nuevaReceta);
    }
    catch (Exception ex)
    {
        return BadRequest(new { message = ex.Message });
    }
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
    [HttpPost("validate")]
[AllowAnonymous]
public async Task<IActionResult> ValidateExternalReceta([FromBody] RecetaConFirmaDto dto)
{
    // Validar que se recibió la firma y la receta
    if (dto == null || string.IsNullOrEmpty(dto.Firma) || dto.Receta == null)
        return BadRequest(new { status = "error", message = "Falta la receta o la firma." });

    // Serializar solo la receta para calcular la firma esperada (sin la firma)
    var jsonReceta = JsonSerializer.Serialize(dto.Receta);

    // Calcular firma esperada
    var secretKey = "HOSPITAL_SECRET_KEY"; // Ideal: mover a configuración segura
    using var sha = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
    var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(jsonReceta));
    var firmaEsperada = Convert.ToBase64String(hash);

    // Comparar firmas
    if (firmaEsperada != dto.Firma)
        return BadRequest(new { status = "error", message = "Firma digital inválida." });

    // Validar datos de receta
    if (dto.Receta.Id == 0 || dto.Receta.RecetaMedicamentos == null || dto.Receta.RecetaMedicamentos.Count == 0)
        return BadRequest(new { status = "error", message = "Datos de receta incompletos." });

    return Ok(new
    {
        status = "ok",
        mensaje = "Receta válida y confirmada",
        codigo = dto.Receta.Id.ToString()
    });
}

[HttpPost("generar-firmada")]
public async Task<IActionResult> GenerarRecetaFirmada([FromBody] Receta receta, int idConsulta)
{
    var nuevaReceta = await _recetaService.AddReceta(receta, idConsulta);

    var json = JsonSerializer.Serialize(nuevaReceta);
    var firma = _firmaService.GenerarFirma(nuevaReceta);

    return Ok(new
    {
        receta = nuevaReceta,
        json,
        firma
    });
}

}