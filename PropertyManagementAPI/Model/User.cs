﻿using System.ComponentModel.DataAnnotations;

namespace PropertyManagementAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }
    }
}
