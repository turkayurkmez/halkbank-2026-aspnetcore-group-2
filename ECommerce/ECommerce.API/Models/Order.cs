namespace ECommerce.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = [];

        public decimal Total() => Items.Sum(x => x.UnitPrice * x.Quantity);
    }
}
