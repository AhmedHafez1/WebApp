using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        public long ProductId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(1,1000)]
        public decimal Price { get; set; }

        [Range(1,long.MaxValue)]
        public long CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, long.MaxValue)]
        public long SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}