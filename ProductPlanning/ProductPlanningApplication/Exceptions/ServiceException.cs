using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductPlanningApplication.Exceptions;

public class ServiceException : Exception
{
    private ServiceException(string message) : base(message) { }

    public static ServiceException RepeatingEntity(string entityName, object?[] keys)
    {
        return new ServiceException($"Entity {entityName} with keys {StringifyKeys(keys)} already exists.");
    }
    
    public static ServiceException RepeatingEntity(string entityName)
        => new ServiceException($"Entity {entityName} already exists.");
    
    public static ServiceException DbSetEntityNotFound(IEntityType entityType, object?[] keys)
        => new ServiceException($"Could not find entity {entityType} with keys {StringifyKeys(keys)}.");

    private static string StringifyKeys(object?[] keys)
    {
        return string.Join(" ", keys);
    }
}