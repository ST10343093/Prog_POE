using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prog_POE.Models
{
    // Represents an agricultural product listed on the platform
    // Contains product details and links to the farmer who produces it
    public class Product
    {
        // Primary key for the product
        [Key]
        public int ProductId { get; set; }

        // Product name with validation to ensure it's provided and not too long
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Product category (e.g., Vegetables, Meat, Dairy) with validation
        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        // Date when the product was produced/harvested
        // DataType attribute ensures proper date handling in forms
        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        // Optional detailed description of the product
        [StringLength(500)]
        public string? Description { get; set; }

        // Product price with specific decimal precision for currency
        // Nullable to allow products without a set price
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        // Flag indicating if the product is organically produced
        // Used for filtering and highlighting sustainable products
        public bool IsOrganic { get; set; }

        // Optional URL to product image
        // Can point to local or external images
        [StringLength(200)]
        public string? ImageUrl { get; set; }

        // Foreign key linking to the farmer who produces this product
        // Establishes relationship with User entity
        public int FarmerId { get; set; }

        // Navigation property to access the related farmer's details
        // Enables easy access to farmer information from a product
        [ForeignKey("FarmerId")]
        public virtual User Farmer { get; set; }
    }
}