using ProductPlanningDomain.Exceptions;

namespace ProductPlanningDomain.Sales.ValueObjects;

public record struct Coefficient
{
    private const decimal MinCoefValue = 0m;
    private const decimal MaxCoefValue = 10m;
    
    public Coefficient(decimal value)
    {
        ValidateValue(value);
        Value = value;
    }

    public decimal Value { get; }
    
    private static void ValidateValue(decimal value)
    {
        if (value is < MinCoefValue or > MaxCoefValue)
        {
            throw ValueObjectException.InvalidValue(value, MinCoefValue, MaxCoefValue);
        }
    }

    public static Coefficient operator *(Coefficient a, Coefficient b)
        => new Coefficient(a.Value * b.Value);
    public static Coefficient operator *(Coefficient a, decimal b)
        => new Coefficient(a.Value * b);
    public static Coefficient operator +(Coefficient a, Coefficient b)
        => new Coefficient(a.Value + b.Value);
}