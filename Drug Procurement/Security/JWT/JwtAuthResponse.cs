namespace Drug_Procurement.Security.JWT;

public class JwtAuthResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public DateTime? Expiration { get; set; }
}
