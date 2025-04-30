using System.ComponentModel.DataAnnotations;

namespace Prog_POE.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // "Farmer" or "Employee"

        // Additional properties for Farmer
        [StringLength(100)]
        public string? FarmName { get; set; }

        [StringLength(200)]
        public string? FarmLocation { get; set; }

        [StringLength(500)]
        public string? FarmDescription { get; set; }

        // Navigation property for Products (if the user is a farmer)
        public virtual ICollection<Product>? Products { get; set; }
    }
}