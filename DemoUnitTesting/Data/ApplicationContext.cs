using DemoUnitTesting.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace DemoUnitTesting.Data
{

    public interface IApplicationContext
    {
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken  = default);
    }

    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Product> Products => Set<Product>();
    }
}
