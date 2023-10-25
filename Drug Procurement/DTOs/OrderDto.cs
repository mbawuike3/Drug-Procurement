using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using System.Text.Json.Serialization;

namespace Drug_Procurement.DTOs;

public class OrderDto 
{
    public string Description { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public double Price { get; set; }
    [JsonIgnore]
    public string? Status { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
