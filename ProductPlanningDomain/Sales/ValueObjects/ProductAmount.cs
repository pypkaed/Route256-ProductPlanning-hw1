using ProductPlanningDomain.Exceptions;
using ProductPlanningDomain.Validators;

namespace ProductPlanningDomain.Sales.ValueObjects;

public record struct ProductAmount
{
    public ProductAmount(decimal value)
    {
        ValueObjectValidator.ValidateProductAmount(value);
        Value = value;
    }

    public ProductAmount()
    { }

    public decimal Value { get; }

    public static ProductAmount operator +(ProductAmount a, ProductAmount b)
    {
        var newValue = a.Value + b.Value;
        ValueObjectValidator.ValidateProductAmount(newValue);
        
        return new ProductAmount(newValue);
    }

    public static ProductAmount operator -(ProductAmount a, ProductAmount b)
    {
        var newValue = a.Value - b.Value;
        ValueObjectValidator.ValidateProductAmount(newValue);
        
        return new ProductAmount(newValue);
    }
}