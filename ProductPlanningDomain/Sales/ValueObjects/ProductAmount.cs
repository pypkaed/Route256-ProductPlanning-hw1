using ProductPlanningDomain.Exceptions;

namespace ProductPlanningDomain.Sales.ValueObjects;

public record struct ProductAmount
{
    private const decimal MinProductAmount = 0m;
    public ProductAmount(decimal value)
    {
        ValidateValue(value);
        Value = value;
    }

    public ProductAmount()
    { }

    public decimal Value { get; }

    public static ProductAmount operator +(ProductAmount a, ProductAmount b)
    {
        var newValue = a.Value + b.Value;
        ValidateValue(newValue);
        
        return new ProductAmount(newValue);
    }

    public static ProductAmount operator -(ProductAmount a, ProductAmount b)
    {
        var newValue = a.Value - b.Value;
        ValidateValue(newValue);
        
        return new ProductAmount(newValue);
    }

    private static void ValidateValue(decimal value)
    {
        if (value < MinProductAmount)
        {
            throw ValueObjectException.InvalidValue(value, MinProductAmount);
        }
    }
}