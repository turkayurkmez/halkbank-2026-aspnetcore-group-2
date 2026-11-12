namespace ECommerce.API.Models.Responses
{
    /*
     *   "id": 3,
    "name": "Kulaklık",
    "sku": "ELK-001",
    "price": 1000.00,
    "stock": 100,
    "categoryId": 7,
    "category": {
      "id": 7,
      "name": "Elektronik",
      "products": [
        null
      ]
    },
     */
    public record ProductListResponse(int Id, string Name, decimal Price, int Stock, string CategoryName);
   
}
