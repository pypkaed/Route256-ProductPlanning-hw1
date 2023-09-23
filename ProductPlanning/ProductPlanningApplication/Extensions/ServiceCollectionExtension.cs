using Microsoft.Extensions.DependencyInjection;
using ProductPlanningApplication.DomainServices.Services;
using ProductPlanningApplication.DomainServices.Services.Interfaces;

namespace ProductPlanningApplication.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IProductPlanningCalculator, ProductPlanningCalculator>();
        collection.AddScoped<IDatabaseService, DatabaseService>();
        collection.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssembly(typeof(IMediatRAssemblyScan).Assembly));
        return collection;
    }
}