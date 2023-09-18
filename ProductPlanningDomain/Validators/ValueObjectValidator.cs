using ProductPlanningDomain.Exceptions;
using ProductPlanningDomain.Sales.ValueObjects;

namespace ProductPlanningDomain.Validators;

public static class ValueObjectValidator
{
    private const int MinProductId = 0;
    private const decimal MinProductAmount = 0m;
    
    private const decimal MinCoefficientValue = 0m;
    private const decimal MaxCoefficientValue = 10m;

    public static void ValidateProductId(int id)
    {
        if (id < MinProductId)
            throw ValueObjectException.InvalidValue(id, MinProductId);
    }
    
    public static void ValidateProductAmountStock(
        ProductAmount amountSold, 
        ProductAmount inStock)
    {
        if (inStock.Value < amountSold.Value)
            throw ValueObjectException.InvalidLogic(less: inStock, more: amountSold);
    }
    
    public static void ValidateProductAmount(decimal value)
    {
        if (value < MinProductAmount)
        {
            throw ValueObjectException.InvalidValue(value, MinProductAmount);
        }
    }
    
    public static void ValidateCoefficientValue(decimal value)
    {
        if (value is < MinCoefficientValue or > MaxCoefficientValue)
        {
            throw ValueObjectException.InvalidValue(value, MinCoefficientValue, MaxCoefficientValue);
        }
    }
}