using Microsoft.Extensions.DependencyInjection;

namespace ProductPlanningApplication.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssembly(typeof(IMediatRAssemblyScan).Assembly));
        return collection;
    }
}