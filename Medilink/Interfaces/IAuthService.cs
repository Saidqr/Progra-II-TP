using Medilink.DTO;
namespace Medilink.Interfaces;

public interface IAuthService
{
    string CreateToken(CreateTokenDto createTokenDto);
    Task<string> Login(LoginDto loginDto);
}