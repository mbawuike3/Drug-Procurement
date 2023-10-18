using System.ComponentModel.DataAnnotations.Schema;

namespace Drug_Procurement.Models
{
    public class Order : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
