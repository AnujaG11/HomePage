using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PawsAndTails.Models
{
    public class Seller : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        [StringLength(300)]
        [MaxLength(300)]
        [Required]
        public string? Address { get; set; }
    }
}
