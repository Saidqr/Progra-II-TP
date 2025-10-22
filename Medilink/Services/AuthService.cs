
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Medilink.DTO;
using Medilink.Interfaces;
using Medilink.Models;

namespace Medilink.Services;

public class AuthService : IAuthService
{
    private readonly Auth _options;
    private readonly IPersonaService _personaService;

    public AuthService(IOptions<Auth> options, IPersonaService personaService)
    {
        _options = options.Value;
        _personaService = personaService;
    }

    public string CreateToken(CreateTokenDto createTokenDto)
    {
        var claims = new List<Claim>
        {
            // Id del usuario
            new(JwtRegisteredClaimNames.Sub, createTokenDto.Id.ToString()),
            // Nombre de login del usuario
            new(JwtRegisteredClaimNames.UniqueName, createTokenDto.NombreUsuario),
            // Nombre del usuario
            new(ClaimTypes.Name, createTokenDto.Nombre)
        };

        // --- Crear la CLAVE de firma ---
        // Se toma la "Key" del archivo de configuración, se pasa a bytes y se crea una SecurityKey.
        // "SymmetricSecurityKey" indica que se usará la misma clave para firmar y validar (HS256).
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

        // --- Crear las CREDENCIALES ---
        // Definen cómo se firmará el token (algoritmo y clave usada).
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // --- Establecer tiempos de validez ---
        var expires = DateTime.UtcNow.AddMinutes(_options.ExpMinutes); // Fecha de expiración
        var notBefore = DateTime.UtcNow;

        // --- Crear el TOKEN ---
        // Se define el token con los datos necesarios.
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires,
            notBefore: notBefore,
            signingCredentials: creds
        );

        // Se convierte el token a formato JWT y se devuelve como string.
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public async Task<string> Login(LoginDto loginDto){
        var persona = await _personaService.GetByNombreUsuario(loginDto.NombreUsuario);
        if (persona == null)
        {
        // Esto evita ataques de enumeración de usuarios
        BCrypt.Net.BCrypt.Verify(loginDto.Contraseña, "$2a$11$000000000000000000000uJ3eF6Z");
        return null;
        }
        // Verificar si la contraseña coincide con el hash almacenado
        if (persona != null && BCrypt.Net.BCrypt.Verify(loginDto.Contraseña, persona.PassHash))
        {
            return CreateToken(new CreateTokenDto(persona.NombreUsuario, persona.Id, persona.Nombre));
        }
        // Si la contraseña no coincide o el usuario no existe se retorna null
        return null;
    }
}