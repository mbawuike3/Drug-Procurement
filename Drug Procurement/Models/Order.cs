using Drug_Procurement.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drug_Procurement.Models
{
    public class Order : BaseEntity
    {
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public double Price { get; set; }
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey(nameof(Inventory))]
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        [DataType(DataType.EmailAddress), StringLength(50)]
        public string Email { get; set; } = string.Empty;
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}
