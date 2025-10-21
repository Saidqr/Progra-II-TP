namespace Medilink.Models
{
    public class Receta 
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public ConsultaMedica Consulta { get; set; }
        public List<(Medicamento, int)> Medicamentos { get; set; }     
    }
}