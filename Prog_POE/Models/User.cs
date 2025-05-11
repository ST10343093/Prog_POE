using System.ComponentModel.DataAnnotations;
namespace Prog_POE.Models
{
    // Represents a user in the system, which can be either a farmer or an employee
    // Contains personal information and role-specific details
    public class User
    {
        // Primary key for the user
        [Key]
        public int UserId { get; set; }

        // Username for login purposes with validation
        // Must be unique across the system
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        // Password for authentication
        // Will be stored in hashed format via PasswordHelper
        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        // User's first name
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        // User's last name
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // Email address with validation to ensure proper format
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        // User role which determines permissions and available features
        // Currently supports "Farmer" or "Employee" roles
        [Required]
        [StringLength(20)]
        public string Role { get; set; } // "Farmer" or "Employee"

        // Additional properties specific to farmers
        // These fields are nullable because they only apply to farmer users

        // Name of the farmer's farm or agricultural business
        [StringLength(100)]
        public string? FarmName { get; set; }

        // Geographical location of the farm (province/region)
        [StringLength(200)]
        public string? FarmLocation { get; set; }

        // Detailed description of the farm and its practices
        [StringLength(500)]
        public string? FarmDescription { get; set; }

        // Navigation property to access all products associated with this farmer
        // Enables easy access to a farmer's product catalog
        // Nullable because employees don't have products
        public virtual ICollection<Product>? Products { get; set; }
    }
}