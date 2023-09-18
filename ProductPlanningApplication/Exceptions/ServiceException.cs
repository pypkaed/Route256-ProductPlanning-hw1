using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductPlanningApplication.Exceptions;

public class ServiceException : Exception
{
    private ServiceException(string message) : base(message) { }

    public static ServiceException RepeatingEntity(string entityName, object?[] keys)
        => new ServiceException($"Entity {entityName} with keys {keys} already exists.");
    public static ServiceException DbSetEntityNotFound(IEntityType entityType, object?[] keys)
        => new ServiceException($"Could not find entity {entityType} with keys {keys}.");
}