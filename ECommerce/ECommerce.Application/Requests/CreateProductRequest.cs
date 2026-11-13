namespace ECommerce.Application.Requests
{
    public record CreateProductRequest(string Name, string Sku, decimal Price, int Stock, int CategoryId);

   
}
