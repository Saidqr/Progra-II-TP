namespace Medilink.Models;
public class Auth
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int ExpMinutes { get; set; }

}