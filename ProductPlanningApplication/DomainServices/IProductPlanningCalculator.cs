using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices;

public interface IProductPlanningCalculator
{
    Task<CalculateAverageDailySalesDto> CalculateAverageDailySalesAsync(int productId, CancellationToken cancellationToken);
    Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(int productId, int numOfDays, CancellationToken cancellationToken);

    Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(
        int productId,
        int numOfDays, 
        DateOnly currentDate,
        CancellationToken cancellationToken);
    Task<CalculateDemandSuppliedDto> CalculateDemandSuppliedAsync(int productId, int numOfDays, DateOnly supplyDate, CancellationToken cancellationToken);
    Task<CalculateDemandDto> CalculateDemandAsync(int productId, int numOfDays, CancellationToken cancellationToken);
}