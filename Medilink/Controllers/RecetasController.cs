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
using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // Asegúrate de tener esta línea
using System.Text;
using System.Security.Cryptography;
using Medilink.Models; // Asegúrate de tener esta línea si RecetaConFirmaDto está aquí
using Medilink.Services; // Asegúrate de tener esta línea si FirmaDigitalService está aquí


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
        var nuevaReceta = await _recetaService.AddReceta(receta, idConsulta);
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
[HttpPost("validar-externa")] // Asegúrate del endpoint correcto
    public async Task<IActionResult> ValidateExternalReceta([FromBody] RecetaConFirmaDto dto)
    {
        // Validar que se recibió la firma y la receta
        if (dto == null || string.IsNullOrEmpty(dto.Firma) || dto.Receta == null)
            return BadRequest(new { status = "error", message = "Falta la receta o la firma." });

        // Serializar solo la receta para calcular la firma esperada (sin la firma)
        // Usar las mismas opciones que en FirmaDigitalService.GenerarFirma
        var jsonReceta = JsonSerializer.Serialize(dto.Receta, _firmaService.GetFirmaOptions()); // <-- Nueva función en el servicio

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

}