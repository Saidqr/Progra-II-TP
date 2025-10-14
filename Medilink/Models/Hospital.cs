namespace Medilink.Models;

public class Hospital
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public List<Medico>? Medicos { get; set; } 
    // public List<Paciente> Pacientes { get; set; }
    // public List<Inventario> Inventarios { get; set; }
}