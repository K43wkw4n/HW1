﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestHW1.Models
{
    public class User
    { 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<Role> Roles { get; set; } 
    }
}
