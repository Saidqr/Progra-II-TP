using System.Text.Json;               // JsonSerializer
using System.Security.Cryptography;   // HMACSHA256
using System.Text;                    // Encoding
using System.Text.Json.Serialization; // JsonSerializerOptions

public class FirmaDigitalService
{
    private readonly string _secretKey;
    private readonly JsonSerializerOptions _firmaOptions; // Opciones específicas para firma

    public FirmaDigitalService(IConfiguration config)
    {
        _secretKey = config["Hospital:SecretKey"] ?? "HOSPITAL_SECRET_KEY";
        // Configurar opciones específicas para la generación de firma
        _firmaOptions = new JsonSerializerOptions
        {
            // Importante: No usar ReferenceHandler.Preserve para la firma
            // ReferenceHandler.Preserve = ReferenceHandler.Preserve; // <- COMENTAR ESTO
            // Opcional: Asegurar consistencia en el orden de propiedades
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // O la que uses globalmente
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // O la que uses globalmente
            WriteIndented = false // Asegura que no haya espacios extra innecesarios
        };
        // Si tienes propiedades específicas que quieres excluir de la firma (además de JsonIgnore),
        // necesitarías un enfoque diferente, como serializar solo un subconjunto de datos.
    }
    public JsonSerializerOptions GetFirmaOptions()
{
    return _firmaOptions; // Devuelve la instancia configurada
}

    public string GenerarFirma(object data)
    {
        // Usar las opciones específicas para la firma
        var json = JsonSerializer.Serialize(data, _firmaOptions);
        using var sha = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(hash);
    }
}