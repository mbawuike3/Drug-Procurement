namespace Drug_Procurement.Models
{
    public class Inventory : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public double Price { get; set; }
    }
}
