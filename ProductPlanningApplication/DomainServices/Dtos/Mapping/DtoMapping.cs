using ProductPlanningDomain.Products;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DomainServices.Dtos.Mapping;

public static class DtoMapping
{
    public static ProductDto AsDto(this Product product)
        => new ProductDto(product.Id);

    public static SeasonalCoefficientDto AsDto(this SeasonalCoefficient coefficient)
        => new SeasonalCoefficientDto(coefficient.ProductId, coefficient.Coefficient.Value, coefficient.Month);

    public static SaleDto AsDto(this Sale sale)
        => new SaleDto(
            sale.Id,
            sale.ProductId,
            sale.Date,
            sale.AmountSold.Value,
            sale.InStock.Value);
}