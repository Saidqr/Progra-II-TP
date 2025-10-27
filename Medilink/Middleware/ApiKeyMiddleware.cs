using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Biblioteca.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Solo procesar si hay header X-API-KEY
        if (context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
        {
            var configuredApiKey = _configuration["ApiKey"];
            
            if (!string.IsNullOrEmpty(configuredApiKey) && configuredApiKey.Equals(apiKey))
            {
                // Crear claims para el usuario API
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "ApiClient"),
                    new Claim(ClaimTypes.Role, "ApiUser"),
                    new Claim("AuthType", "ApiKey")
                };

                var identity = new ClaimsIdentity(claims, "ApiKey");
                var principal = new ClaimsPrincipal(identity);
                
                context.User = principal;
            }
        }

        await _next(context);
    }
}