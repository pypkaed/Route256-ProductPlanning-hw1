using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices;

public interface IProductPlanningCalculator
{
    Task<CalculateAverageDailySalesDto> CalculateAverageDailySalesAsync(int productId, CancellationToken cancellationToken);
    Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(int productId, int numOfDays, CancellationToken cancellationToken);

    Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(
        int productId,
        int numOfDays, 
        DateTime currentDate,
        CancellationToken cancellationToken);
    Task<CalculateDemandSuppliedDto> CalculateDemandSuppliedAsync(int productId, int numOfDays, DateTime supplyDate, CancellationToken cancellationToken);
    Task<CalculateDemandDto> CalculateDemandAsync(int productId, int numOfDays, CancellationToken cancellationToken);
}