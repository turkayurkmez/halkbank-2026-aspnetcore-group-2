using ECommerce.Domain;

namespace ECommerce.Domain
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        public decimal Total() => Items.Sum(x => x.UnitPrice * x.Quantity);
        public Customer Customer { get; set; }

     
    }
}
