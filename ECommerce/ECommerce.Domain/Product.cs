

using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ürün adı boş olamaz")]
        [MaxLength(200, ErrorMessage ="En fazla 200 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
