using ProductPlanningDomain.Exceptions;

namespace ProductPlanningDomain.Sales;

public record struct Coefficient
{
    private const decimal MinCoefValue = 0;
    private const decimal MaxCoefValue = 10;
    
    public Coefficient(int value)
    {
        ValidateValue(value);
        Value = value;
    }

    public int Value { get; }
    
    private static void ValidateValue(int value)
    {
        if (value < MinCoefValue || value > MaxCoefValue)
        {
            throw ValueObjectException.InvalidValue(value, MinCoefValue, MaxCoefValue);
        }
    }
}