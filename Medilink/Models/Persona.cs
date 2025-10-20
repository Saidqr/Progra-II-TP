namespace Medilink.Models
{
    public class Persona 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public List<Rol> Roles { get; set; }
    }
}