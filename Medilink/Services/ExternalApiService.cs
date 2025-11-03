using System.Text.Json;
using Medilink.Models;

namespace Medilink.Services;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalApiService> _logger;

    public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<PersonaDto>?> GetPersonasAsync()
    {
        try
        {
            _logger.LogInformation("Haciendo request a la API externa para obtener personas...");
            
            // Primero intentar con API Key
            var fullUrl = _httpClient.BaseAddress != null 
                ? new Uri(_httpClient.BaseAddress, "api/personas").ToString()
                : "http://localhost:6003/api/personas";
            
            _logger.LogInformation("URL final que se usará: {FullUrl}", fullUrl);
            
            var response = await _httpClient.GetAsync(fullUrl);
            
            _logger.LogInformation("Response Status: {StatusCode}", response.StatusCode);
            _logger.LogInformation("Response Headers: {Headers}", string.Join(", ", response.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}")));
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Respuesta exitosa de la API externa: {Content}", content);
                
                var personas = JsonSerializer.Deserialize<List<PersonaDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return personas;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error en la API externa. Status: {StatusCode}, Reason: {ReasonPhrase}, Content: {Content}", 
                    response.StatusCode, response.ReasonPhrase, errorContent);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al hacer request a la API externa");
            return null;
        }
    }
}
