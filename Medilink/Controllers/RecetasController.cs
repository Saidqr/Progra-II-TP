using Medilink.Models;
using Medilink.Services;
using Medilink.Interfaces;
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
    public async Task<ActionResult<Receta>> Create([FromBody] Receta receta, int idConsulta)
    {
        var nuevaReceta = await _recetaService.AddReceta(receta,idConsulta);
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
    [HttpPost("validate")]
[AllowAnonymous] // permite que la farmacia lo use sin token
public async Task<IActionResult> ValidateExternalReceta([FromBody] JsonElement body)
{
    // 1️⃣ Leer la firma enviada en el header
    if (!Request.Headers.TryGetValue("X-Hospital-Signature", out var firmaRecibida))
        return BadRequest(new { status = "error", message = "Falta la firma en el header." });

    // 2️⃣ Obtener el JSON exacto
    var json = body.GetRawText();

    // 3️⃣ Calcular la firma esperada
    var secretKey = "HOSPITAL_SECRET_KEY"; // ⚠️ Mover a configuración segura (appsettings.json o secrets)
    using var sha = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
    var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
    var firmaEsperada = Convert.ToBase64String(hash);

    // 4️⃣ Comparar firmas
    if (firmaEsperada != firmaRecibida)
        return BadRequest(new { status = "error", message = "Firma digital inválida." });

    // 5️⃣ Deserializar la receta recibida
    Receta? receta;
    try
    {
        receta = JsonSerializer.Deserialize<Receta>(json);
    }
    catch
    {
        return BadRequest(new { status = "error", message = "Formato de receta inválido." });
    }

    if (receta == null || string.IsNullOrWhiteSpace(receta.CodigoReceta))
        return BadRequest(new { status = "error", message = "Datos de receta incompletos." });

    // 6️⃣ (Opcional) Guardar la receta validada en la base de datos
    // Esto es útil si querés tener registro de las recetas validadas externamente
    // await _recetaService.AddReceta(receta, idConsulta: 0); // solo si querés persistirla

    // 7️⃣ Devolver OK a la farmacia
    return Ok(new
    {
        status = "ok",
        mensaje = "Receta válida y confirmada",
        codigo = receta.CodigoReceta
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