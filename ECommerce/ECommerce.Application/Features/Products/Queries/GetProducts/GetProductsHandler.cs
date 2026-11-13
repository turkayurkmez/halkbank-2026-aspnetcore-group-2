using ECommerce.Application.Responses;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsHandler(IUnitOfWork uow) : IRequestHandler<GetProductsQuery, IEnumerable<ProductListResponse>>
    {
        public async Task<IEnumerable<ProductListResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await uow.Products.GetAllAsync();

            return products.Adapt<IEnumerable<ProductListResponse>>();
        }
    }
}
