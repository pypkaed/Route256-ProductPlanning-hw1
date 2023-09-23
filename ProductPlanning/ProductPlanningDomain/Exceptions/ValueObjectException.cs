namespace ProductPlanningDomain.Exceptions;

public class ValueObjectException : DomainException
{
    private ValueObjectException(string message) 
        : base(message) { }

    public static ValueObjectException InvalidValue<TValue>(TValue value, TValue min, TValue max)
    where TValue : struct
        => new ValueObjectException($"Value {value} must be in range [{min}, {max}].");
    
    public static ValueObjectException InvalidValue<TValue>(TValue value, TValue min)
    where TValue : struct
        => new ValueObjectException($"Value {value} must be greater than {min}.");
    
    public static ValueObjectException InvalidLogic<TValue>(TValue less, TValue more)
    where TValue : struct
        => new ValueObjectException($"Value {less} must be greater than {more}.");
}