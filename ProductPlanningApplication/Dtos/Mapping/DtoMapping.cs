using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.Dtos.Mapping;

public static class DtoMapping
{
    public static SeasonalCoefficientDto AsDto(this SeasonalCoefficient coefficient)
        => new SeasonalCoefficientDto(coefficient.ProductId, coefficient.Coefficient.Value, coefficient.Month);

    public static SaleDto AsDto(this Sale sale)
        => new SaleDto(
            sale.ProductId,
            sale.Date,
            sale.AmountSold.Value,
            sale.InStock.Value);
}