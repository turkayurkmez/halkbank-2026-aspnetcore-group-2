using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    //1. Create için ne gerekir?
   
    public record CreateProductCommand(string Name, string Sku, decimal Price, int Stock, int CategoryId) : IRequest<CreateProductResponse>;

    //2. Create ne döndürür?

    public record CreateProductResponse(int Id, string Name, string Sku, decimal Price, int Stock, int CategoryId);

}
