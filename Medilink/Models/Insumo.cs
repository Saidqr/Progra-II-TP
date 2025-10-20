namespace Medilink.Models;
public abstract class Insumo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int cantidadInventario { get; set; }
}