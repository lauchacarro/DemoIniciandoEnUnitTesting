using DemoUnitTesting.Data;
using DemoUnitTesting.Domain;
using DemoUnitTesting.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace DemoUnitTesting.Application.Mediatr.AddProduct
{
    public class AddProductRequestHandler : IRequestHandler<AddProductRequest, Result<AddProductResponse>>
    {
        private readonly IApplicationContext _context;

        public AddProductRequestHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<AddProductResponse>> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            if (request.Name.Length < 3)
            {
                return "ERROR_CODE_A1";
            }

            if (request.Description is not null && request.Description.Length < 3)
            {
                return "ERROR_CODE_A2";
            }

            if (request.Price <= 0)
            {
                return "ERROR_CODE_A3";
            }

            bool existProductName = await _context.Products.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (existProductName)
            {
                return "ERROR_CODE_A4";
            }

            Product product = new(request.Name, request.Description, request.Price, true);

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return new AddProductResponse(product.Id, product.Name, product.Description, product.Price, product.IsActive);
        }
    }
}
