﻿using System.Text.Json.Serialization;

namespace Drug_Procurement.DTOs
{
    public class UserCreationDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [JsonIgnore]
        public int RoleId { get; set; }
    }
}