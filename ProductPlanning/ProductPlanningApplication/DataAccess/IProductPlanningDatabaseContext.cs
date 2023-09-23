using Microsoft.EntityFrameworkCore;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DataAccess;

public interface IProductPlanningDatabaseContext
{
    DbSet<Sale> Sales { get; }
    DbSet<SeasonalCoefficient> SeasonalCoefficients { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}