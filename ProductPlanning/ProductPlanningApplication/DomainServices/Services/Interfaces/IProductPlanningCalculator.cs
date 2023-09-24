using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.Services.Interfaces;

public interface IProductPlanningCalculator
{
    Task<CalculateAverageDailySalesDto> CalculateAverageDailySales(
        int productId,
        CancellationToken cancellationToken);
    Task<CalculateSalesPredictionDto> CalculateSalesPrediction(
        int productId,
        int numOfDays,
        CancellationToken cancellationToken);

    Task<CalculateSalesPredictionDto> CalculateSalesPrediction(
        int productId,
        int numOfDays, 
        DateOnly currentDate,
        CancellationToken cancellationToken);
    Task<CalculateDemandSuppliedDto> CalculateDemandSupplied(
        int productId,
        int numOfDays,
        DateOnly supplyDate, 
        CancellationToken cancellationToken);
    Task<CalculateDemandDto> CalculateDemand(
        int productId,
        int numOfDays,
        CancellationToken cancellationToken);
}