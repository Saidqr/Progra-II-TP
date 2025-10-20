namespace Medilink.Models;

public class Medicamento
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaFabricacion { get; set; }
    public DateTime FechaVencimiento { get; set; } 
}