﻿using System.ComponentModel.DataAnnotations;

namespace SPSA.API.Domain.Dtos
{
    public class AuthinticateDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
