using ProductPlanningDomain.Sales.ValueObjects;
using ProductPlanningDomain.Validators;

namespace ProductPlanningDomain.Sales;

public class SeasonalCoefficient
{
    public SeasonalCoefficient(
        int productId,
        Coefficient coefficient,
        int month)
    {
        ValueObjectValidator.ValidateProductId(productId);
        ProductId = productId;
        Coefficient = coefficient;
        Month = month;
    }

    public SeasonalCoefficient()
    { }

    public int ProductId { get; init; }

    public Coefficient Coefficient { get; init; }

    public int Month { get; init; }
}