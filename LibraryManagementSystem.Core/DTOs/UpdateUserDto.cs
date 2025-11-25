using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Core.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
