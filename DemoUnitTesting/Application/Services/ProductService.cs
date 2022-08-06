using DemoUnitTesting.Data;
using DemoUnitTesting.Domain;
using DemoUnitTesting.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace DemoUnitTesting.Application.Services
{
    public interface IProductService
    {
        Task<Result<Product>> GetByIdAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IApplicationContext _context;
        private readonly IHttpContextAccessor _accessor;

        public ProductService(IApplicationContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<Result<Product>> GetByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return "ERROR_CODE_G1";
            }

            return product;
        }
    }
}
