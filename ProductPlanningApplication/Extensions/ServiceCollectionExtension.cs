using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductPlanningApplication.DomainServices;

namespace ProductPlanningApplication.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IProductPlanningCalculator, ProductPlanningCalculator>();
        collection.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssembly(typeof(IMediatRAssemblyScan).Assembly));
        return collection;
    }
}