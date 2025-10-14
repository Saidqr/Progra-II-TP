namespace Medilink.Models
{
    public class Persona 
    {
        public int Id { get; set; }
        public IRol? Rol { get; set; }
        public string? Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Mail { get; set; }
    }
}