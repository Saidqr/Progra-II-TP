namespace Medilink.Models
{
    public class Persona 
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string NombreUsuario { get; set; }
        public required string PassHash { get; set; }
        public required string DNI { get; set; }
        public required DateTime fechaNacimiento { get; set; }
        public List<Rol>? Roles { get; set; }
    }
}