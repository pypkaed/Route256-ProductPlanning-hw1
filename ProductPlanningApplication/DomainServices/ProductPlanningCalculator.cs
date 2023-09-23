using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.Dtos;
using ProductPlanningApplication.Exceptions;
using ProductPlanningApplication.Extensions;

namespace ProductPlanningApplication.DomainServices;

public class ProductPlanningCalculator : IProductPlanningCalculator
{
    private readonly IProductPlanningDatabaseContext _databaseContext;

    public ProductPlanningCalculator(IProductPlanningDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<CalculateAverageDailySalesDto> CalculateAverageDailySalesAsync(
        int productId,
        CancellationToken cancellationToken)
    {
        var sales = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .ToListAsync(cancellationToken);
        if (!sales.Any())
            throw ServiceException.DbSetEntityNotFound(
                _databaseContext.Sales.EntityType,
                new object?[] { productId });

        var amountSold = sales.Select(s => s.AmountSold.Value).Sum();
        var numOfDaysInStock = sales.Count(s => s.InStock.Value > 0);
        if (numOfDaysInStock == 0)
            throw CalculationsException.NotEnoughStock(productId);
        
        var averageDailySales = amountSold / numOfDaysInStock;
        
        return new CalculateAverageDailySalesDto(averageDailySales);
    }

    public async Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(int productId, int numOfDays, CancellationToken cancellationToken)
    {
        var averageDailySales = await CalculateAverageDailySalesAsync(productId, cancellationToken);
        var currentDate = DateTime.Now;
        var monthsInFuture = Enumerable.Range(0, numOfDays)
            .Select(offset => currentDate.AddDays(offset))
            .GroupBy(date => date.Month)
            .Select(group => new
            {
                Month = group.Key,
                DaysInMonth = (decimal)group.Count()
            });

        decimal coefficient = 0;
        foreach (var month in monthsInFuture)
        {
            var seasonalCoefficient = await _databaseContext.SeasonalCoefficients.GetEntityAsync(
                new object?[]{ productId, month.Month },
                cancellationToken);
            coefficient += seasonalCoefficient.Coefficient.Value * month.DaysInMonth;
        }

        var salesPrediction = averageDailySales.AverageDailySales * coefficient;
        return new CalculateSalesPredictionDto(salesPrediction);
    }
    public async Task<CalculateSalesPredictionDto> CalculateSalesPredictionAsync(
        int productId,
        int numOfDays,
        DateOnly currentDate,
        CancellationToken cancellationToken)
    {
        var averageDailySales = await CalculateAverageDailySalesAsync(productId, cancellationToken);
        var monthsInFuture = Enumerable.Range(0, numOfDays)
            .Select(offset => currentDate.AddDays(offset))
            .GroupBy(date => date.Month)
            .Select(group => new
            {
                Month = group.Key,
                DaysInMonth = (decimal)group.Count()
            });

        decimal coefficient = 0;
        foreach (var month in monthsInFuture)
        {
            var seasonalCoefficient = await _databaseContext.SeasonalCoefficients.GetEntityAsync(
                new object?[]{ productId, month.Month },
                cancellationToken);
            coefficient += seasonalCoefficient.Coefficient.Value * month.DaysInMonth;
        }

        var salesPrediction = averageDailySales.AverageDailySales * coefficient;
        return new CalculateSalesPredictionDto(salesPrediction);
    }

    public async Task<CalculateDemandSuppliedDto> CalculateDemandSuppliedAsync(
        int productId,
        int numOfDays,
        DateOnly supplyDate,
        CancellationToken cancellationToken)
    {
        var predictionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(numOfDays));
        if (predictionDate < supplyDate)
            throw new Exception("Supply date will be after the asked prediction");

        var dateOnlyNow = DateOnly.FromDateTime(DateTime.Now);
        var daysUntilSupply = supplyDate.DayNumber - dateOnlyNow.DayNumber;
        var salesPredictionSupply = await CalculateSalesPredictionAsync(productId, daysUntilSupply, cancellationToken);

        var lastSaleProductEntry = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .OrderBy(s => s.Date)
            .LastAsync(cancellationToken);
        
        if (lastSaleProductEntry is null)
            throw ServiceException.DbSetEntityNotFound(
                _databaseContext.Sales.EntityType,
                new object?[] { productId });
        
        var stockSupplied = lastSaleProductEntry.InStock.Value - salesPredictionSupply.SalesPrediction;
        stockSupplied = stockSupplied > 0 ? stockSupplied : 0;
        

        var salesPrediction = await CalculateSalesPredictionAsync(
            productId,
            numOfDays - daysUntilSupply,
            currentDate: dateOnlyNow.AddDays(daysUntilSupply),
            cancellationToken);
        var demand = salesPrediction.SalesPrediction - stockSupplied;
        
        return new CalculateDemandSuppliedDto(demand > 0 ? demand : 0);
    }
    
    public async Task<CalculateDemandDto> CalculateDemandAsync(
        int productId, 
        int numOfDays,
        CancellationToken cancellationToken)
    {
        var lastSaleProductEntry = await _databaseContext.Sales
            .Where(s => s.ProductId == productId)
            .OrderBy(s => s.Date)
            .LastAsync(cancellationToken);
        
        if (lastSaleProductEntry is null)
            throw ServiceException.DbSetEntityNotFound(
                _databaseContext.Sales.EntityType,
                new object?[] { productId });
        
        var productAmountInStock = lastSaleProductEntry.InStock;

        var salesPrediction = await CalculateSalesPredictionAsync(productId, numOfDays, cancellationToken);
        var demand = salesPrediction.SalesPrediction - productAmountInStock.Value;

        return new CalculateDemandDto(demand > 0 ? demand : 0);
    }
}