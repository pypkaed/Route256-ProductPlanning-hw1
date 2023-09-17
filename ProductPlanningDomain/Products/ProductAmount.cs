using ProductPlanningDomain.Exceptions;

namespace ProductPlanningDomain.Products;

public record struct ProductAmount
{
    private const int MinProductAmount = 0;
    public ProductAmount(int value)
    {
        ValidateValue(value);
        Value = value;
    }

    public int Value { get; }

    public static ProductAmount operator +(ProductAmount a, ProductAmount b)
    {
        int newValue = a.Value + b.Value;
        ValidateValue(newValue);
        
        return new ProductAmount(newValue);
    }

    public static ProductAmount operator -(ProductAmount a, ProductAmount b)
    {
        int newValue = a.Value - b.Value;
        ValidateValue(newValue);
        
        return new ProductAmount(newValue);
    }

    private static void ValidateValue(int value)
    {
        if (value < MinProductAmount)
        {
            throw ValueObjectException.InvalidValue(value, MinProductAmount);
        }
    }
}