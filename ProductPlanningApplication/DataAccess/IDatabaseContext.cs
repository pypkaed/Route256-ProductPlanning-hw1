using Microsoft.EntityFrameworkCore;
using ProductPlanningDomain.Products;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DataAccess;

public interface IDatabaseContext
{
    DbSet<Product> Products { get; }
    DbSet<Sale> Sales { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}