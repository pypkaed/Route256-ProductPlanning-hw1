using ProductPlanningDomain.Validators;

namespace ProductPlanningDomain.Sales.ValueObjects;

public record struct Coefficient
{
    public Coefficient(decimal value)
    {
        ValueObjectValidator.ValidateCoefficientValue(value);
        Value = value;
    }

    public Coefficient()
    { }

    public decimal Value { get; }

    public static Coefficient operator *(Coefficient a, Coefficient b)
        => new Coefficient(a.Value * b.Value);
    public static Coefficient operator *(Coefficient a, decimal b)
        => new Coefficient(a.Value * b);
    public static Coefficient operator +(Coefficient a, Coefficient b)
        => new Coefficient(a.Value + b.Value);
}