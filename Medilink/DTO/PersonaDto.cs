namespace Medilink.DTO;
public record RegistrarPersonaDto(string Nombre, string NombreUsuario, string Contrasenia, string Apellido, DateTime fechaNacimiento, string DNI);
public record ActualizarPersonaDto(string Nombre, string NombreUsuario, string Apellido, DateTime fechaNacimiento, string DNI);
public record PersonaDto(string Nombre, string NombreUsuario, string Apellido, DateTime fechaNacimiento, string DNI);