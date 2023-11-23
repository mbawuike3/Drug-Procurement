namespace Drug_Procurement.Models
{
    public class LoginRefreshToken : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
        public bool IsActive { get; set; }
    }
}
