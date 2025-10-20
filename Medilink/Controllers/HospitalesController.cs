using System.Runtime.Versioning;
using Medilink.Interfaces;
using Medilink.Models;
using Medilink.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medilink.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HospitalesController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalesController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hospital>>> GetAll()
    {
        var hospitales = await _hospitalService.GetHospitals();
        return Ok(hospitales);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hospital>> GetOneById(int id)
    {
        var hospital = await _hospitalService.GetHospital(id);
        if (hospital == null) return NotFound();
        return Ok(hospital);
    }

    [HttpPost]
    public async Task<ActionResult<Hospital>> Create([FromBody] Hospital hospital)
    {
        var nuevoHospital = await _hospitalService.AddHospital(hospital);
        return CreatedAtAction(nameof(GetOneById), new { id = nuevoHospital.Id }, nuevoHospital);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> CompleteUpdate([FromBody] Hospital hospital, int id)
    {
        if (id != hospital.Id) return BadRequest();
        var resultado = await _hospitalService.UpdateHospital(hospital);
        if (!resultado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var hospital = await _hospitalService.DeleteHospital(id);
        if (!hospital) return NotFound($"No se encontró el hospital con el AID {id}");
        else return NoContent();
    }
}