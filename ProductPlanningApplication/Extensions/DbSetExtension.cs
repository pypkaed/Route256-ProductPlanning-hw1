using Microsoft.EntityFrameworkCore;

namespace ProductPlanningApplication.Extensions;

public static class DbSetExtension
{
    public static async Task<T> GetEntityAsync<T>(
        this DbSet<T> dbSet,
        object?[] keys,
        CancellationToken cancellationToken)
    where T : class
    {
        var entity = await dbSet.FindAsync(
            keys,
            cancellationToken);

        if (entity is null)
            throw new Exception();

        return entity;
    }
}