namespace Medilink.DTO;
public record RolDto();
public record RegistrarPersonaDto(string Nombre, string NombreUsuario, string Contrasenia, string Apellido, DateTime fechaNacimiento, string DNI,List<int> IdRoles);
public record ActualizarPersonaDto(string Nombre, string NombreUsuario, string Apellido, DateTime fechaNacimiento, string DNI,List<int> IdRoles);
public record PersonaDto(string Nombre, string NombreUsuario, string Apellido, DateTime fechaNacimiento, string DNI,List<Rol> roles);