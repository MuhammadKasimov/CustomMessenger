﻿using CustomMessenger.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.DTO.Users
{
    public class UserForCreation
    {
        [Required, MaxLength(60)]
        public string Name { get; set; }
        [Required, StrongPassword]
        public string Password { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required, ValidUniquename]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Bio { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
