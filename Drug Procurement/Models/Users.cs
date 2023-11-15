using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Drug_Procurement.Models
{
    public class Users : BaseEntity
    {
        [StringLength(70)]
        public string UserName { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress), StringLength(70)]
        public string Email { get; set; } = string.Empty;
        [ForeignKey(nameof(Roles))]
        public int RoleId { get; set; }
        public Roles Roles { get; set; }
        [JsonIgnore]
        public string? Salt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
