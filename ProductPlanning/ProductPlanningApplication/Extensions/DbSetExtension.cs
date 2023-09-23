using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.Exceptions;

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
            throw ServiceException.DbSetEntityNotFound(dbSet.EntityType, keys);

        return entity;
    }
}