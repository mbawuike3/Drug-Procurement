using System.ComponentModel.DataAnnotations.Schema;

namespace Drug_Procurement.Models
{
    public class Inventory : BaseEntity
    {
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public Users User { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public double Price { get; set; }
    }
}
