using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningDomain.Products;
using ProductPlanningDomain.Sales;

namespace ProductPlanningDataAccess;

internal class ProductPlanningDatabaseContext : DbContext, IProductPlanningDatabaseContext
{
    public ProductPlanningDatabaseContext(DbContextOptions<ProductPlanningDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Product> Products { get; }
    public DbSet<Sale> Sales { get; }
    public DbSet<SeasonalCoefficient> SeasonalCoefficients { get; }
}