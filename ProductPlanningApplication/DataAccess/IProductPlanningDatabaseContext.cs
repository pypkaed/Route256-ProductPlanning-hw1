using Microsoft.EntityFrameworkCore;
using ProductPlanningDomain.Products;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DataAccess;

public interface IProductPlanningDatabaseContext
{
    DbSet<Product> Products { get; }
    DbSet<Sale> Sales { get; }
    DbSet<SeasonalCoefficient> SeasonalCoefficients { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}