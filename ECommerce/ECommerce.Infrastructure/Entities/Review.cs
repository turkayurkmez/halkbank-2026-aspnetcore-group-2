namespace ECommerce.Infrastructure.Entities
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }

}
