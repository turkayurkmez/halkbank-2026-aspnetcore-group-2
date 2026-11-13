using ECommerce.Domain;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler(IUnitOfWork uow) : IRequestHandler<CreateProductCommand,CreateProductResponse>
    {
      

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();
            await uow.Products.AddAsync(product);
            await uow.SaveChangesAsync();

            return product.Adapt<CreateProductResponse>();
        }
    }
}
