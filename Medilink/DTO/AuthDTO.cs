namespace Medilink.DTO;
public record LoginDto(string NombreUsuario, string Contraseña);
public record LoginResponseDto(string Token, string NombreUsuario);
public record CreateTokenDto(string NombreUsuario, int Id, string Nombre);