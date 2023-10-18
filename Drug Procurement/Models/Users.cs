using System.ComponentModel.DataAnnotations.Schema;

namespace Drug_Procurement.Models
{
    public class Users : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Roles Roles { get; set; }


    }
}
