using System.Text.Json;               // JsonSerializer
using System.Security.Cryptography;   // HMACSHA256
using System.Text;                    // Encoding

public class FirmaDigitalService
{
    private readonly string _secretKey;

    public FirmaDigitalService(IConfiguration config)
    {
        _secretKey = config["Hospital:SecretKey"] ?? "HOSPITAL_SECRET_KEY";
    }

    public string GenerarFirma(object data)
    {
        var json = JsonSerializer.Serialize(data);
        using var sha = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(hash);
    }
}
