namespace Medilink.Models
{
    public class Receta
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdConsulta { get; set; }
        public ConsultaMedica Consulta {get;set;}
        public ICollection<RecetaMedicamento> RecetaMedicamentos { get; set; }
    }
    
    public class RecetaMedicamento
    {
        public int Id { get; set; }
        public int IdReceta { get; set; }
        public int IdMedicamento { get; set; }
        public Medicamento medicamento { get; set; }
        public int Cantidad { get; set; }
    }
}