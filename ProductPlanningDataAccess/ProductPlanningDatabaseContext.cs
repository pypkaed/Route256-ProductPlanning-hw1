using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningDomain.Sales;

namespace ProductPlanningDataAccess;

internal class ProductPlanningDatabaseContext : DbContext, IProductPlanningDatabaseContext
{
    public ProductPlanningDatabaseContext(DbContextOptions<ProductPlanningDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SeasonalCoefficient>()
            .HasKey(c => new { c.ProductId, c.Month });
    }

    public DbSet<Sale> Sales { get; private init; } = null!;
    public DbSet<SeasonalCoefficient> SeasonalCoefficients { get; private init; } = null!;
}