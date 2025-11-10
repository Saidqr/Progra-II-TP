using System.Text.Json.Serialization;
namespace Medilink.Models
{
    public class Receta
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public ICollection<RecetaMedicamento> RecetaMedicamentos { get; set; }
    }

    public class RecetaMedicamento
    {
        public int Id { get; set; }
        public int IdReceta { get; set; }
        public int IdMedicamento { get; set; }
        [JsonIgnore]
        public Medicamento medicamento { get; set; }
        public int Cantidad { get; set; }
    }
}