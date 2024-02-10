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
        public string Name { get; set; }=string.Empty;
        public decimal Price { get; set; }

        [Column(TypeName ="varchar(100)")]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public Category? Category { get; set; }
    }
}
