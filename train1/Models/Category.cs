using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace train1.Models
{
    public class Category
    {
        [Key]
        public  int Id { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; } = string.Empty;
        
        public List<Product>? Products { get; set; }
    }
}
