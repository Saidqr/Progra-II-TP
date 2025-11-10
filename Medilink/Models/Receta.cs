using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [JsonIgnore]
        public int IdReceta { get; set; }
        public int IdMedicamento { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Medicamento Medicamento { get; set; }
        public int Cantidad { get; set; }
    }
}