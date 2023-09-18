using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices;

public interface IDatabaseService
{
    Task<SaleDto> CreateSale(
        int productId,
        DateTime date,
        decimal sales,
        decimal stock,
        CancellationToken cancellationToken);

    Task<SeasonalCoefficientDto> CreateSeasonalCoefficient(
        int productId,
        int month,
        decimal coeff,
        CancellationToken cancellationToken);
}