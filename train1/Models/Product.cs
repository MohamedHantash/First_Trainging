using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace train1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string Name { get; set; }=string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string? ImageURl { get; set; } = string.Empty;

        [Column(TypeName ="varchar(100)")]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Category")]
        [Display(Name ="Category")]
        public int Category_Id { get; set; }
        public Category? Category { get; set; }
    }
}
