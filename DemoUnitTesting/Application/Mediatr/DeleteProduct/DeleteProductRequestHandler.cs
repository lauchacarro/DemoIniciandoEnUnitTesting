using DemoUnitTesting.Data;
using DemoUnitTesting.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace DemoUnitTesting.Application.Mediatr.DeleteProduct
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Result>
    {
        private readonly IApplicationContext _context;

        public DeleteProductRequestHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product is null)
            {
                return "ERROR_CODE_D1";
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
