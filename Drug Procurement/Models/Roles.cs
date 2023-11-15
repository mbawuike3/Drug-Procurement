﻿namespace Drug_Procurement.Models
{
    public class Roles : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
