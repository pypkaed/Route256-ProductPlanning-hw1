using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductPlanningApplication.DataAccess;

namespace ProductPlanningDataAccess.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> config)
    {
        collection.AddDbContext<ProductPlanningDatabaseContext>(config);
        collection.AddScoped<IProductPlanningDatabaseContext>(sp =>
            sp.GetRequiredService<ProductPlanningDatabaseContext>());

        return collection;
    }
}