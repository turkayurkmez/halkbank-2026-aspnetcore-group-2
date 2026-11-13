using ECommerce.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductListResponse>>
    {
    }
}
