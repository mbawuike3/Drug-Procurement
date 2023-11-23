namespace Drug_Procurement.Security.Jwt;

public class JwtAuthResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
    public string? Message { get; set; }
}
