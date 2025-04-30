using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog_POE.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public bool IsOrganic { get; set; }

        [StringLength(200)]
        public string? ImageUrl { get; set; }

        // Foreign key for User (Farmer)
        public int FarmerId { get; set; }

        // Navigation property for User
        [ForeignKey("FarmerId")]
        public virtual User Farmer { get; set; }
    }
}