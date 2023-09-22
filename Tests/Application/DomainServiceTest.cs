using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices;
using ProductPlanningDataAccess;
using Xunit;

namespace Tests.Application;

public class DomainServiceTest
{
    private readonly IProductPlanningDatabaseContext _context;
    private readonly IDatabaseService _databaseService;
    private readonly IProductPlanningCalculator _calculator;

    public DomainServiceTest()
    {
        DbContextOptions<ProductPlanningDatabaseContext> options =
            new DbContextOptionsBuilder<ProductPlanningDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        _context = new ProductPlanningDatabaseContext(options);

        _databaseService = new DatabaseService(_context);
        _calculator = new ProductPlanningCalculator(_context);
    }

    [Fact]
    public async Task CalculateAds()
    {
        await _databaseService.CreateSale(1, DateTime.Now, 10, 50, CancellationToken.None);
        await _databaseService.CreateSale(1, DateTime.Now.AddDays(1), 5, 40, CancellationToken.None);

        var ads = await _calculator.CalculateAverageDailySalesAsync(1, CancellationToken.None);
        
        Assert.Equal(7.5m, ads.AverageDailySales);
    }
    
    [Fact]
    public async Task CalculateSalesPrediction()
    {
        // 12 days in September with seasonal coefficient of 0.1,
        // 1 day in November with seasonal coefficient 10
        await ArrangeSales(productId: 1);
        await ArrangeSeasonalCoefficients(productId: 1);

        var ads = await _calculator.CalculateAverageDailySalesAsync(productId: 1, CancellationToken.None);
        var salesPrediction = await _calculator.CalculateSalesPredictionAsync(
            productId: 1,
            numOfDays: 13,
            currentDate: DateTime.Parse("09/19/2023"),
            CancellationToken.None);
        
        Assert.Equal(ads.AverageDailySales * ((12m * 0.1m) + (1m * 10m)), salesPrediction.SalesPrediction);
    }

    [Fact]
    public async Task CalculateDemandNow()
    {
        // in future:
        // 12 days in September with seasonal coefficient of 0.1,
        // 1 day in November with seasonal coefficient 10
        // last stock: 100
        await ArrangeSales(productId: 1);
        await ArrangeSeasonalCoefficients(productId: 1);
        
        var salesPrediction = await _calculator.CalculateSalesPredictionAsync(
            productId: 1,
            numOfDays: 13,
            CancellationToken.None);
        var demand = await _calculator.CalculateDemandAsync(
            productId: 1,
            numOfDays: 13,
            CancellationToken.None);
        var expected = salesPrediction.SalesPrediction - 100;
        expected = expected > 0 ? expected : 0;
        
        Assert.Equal(expected, demand.Demand);
    }
    
    [Fact]
    public async Task CalculateDemandSupplied()
    {
        // in future:
        // 12 days in September with seasonal coefficient of 0.1,
        // 1 day in November with seasonal coefficient 10
        // last stock: 100
        await ArrangeSales(productId: 1);
        await ArrangeSeasonalCoefficients(productId: 1);

        var supplySalesPrediction = await _calculator.CalculateSalesPredictionAsync(
            productId: 1,
            numOfDays: 5,
            CancellationToken.None);
        var stockSupplied = 100 - supplySalesPrediction.SalesPrediction;
        var salesPrediction = await _calculator.CalculateSalesPredictionAsync(
            productId: 1,
            numOfDays: 13 - 5,
            currentDate: DateTime.Now.AddDays(5),
            CancellationToken.None);
        
        var demand = await _calculator.CalculateDemandSuppliedAsync(
            productId: 1,
            numOfDays: 13,
            supplyDate: DateTime.Parse("09/25/2023"),
            CancellationToken.None);

        var expected = salesPrediction.SalesPrediction - stockSupplied;
        
        Assert.Equal(expected, demand.Demand);
    }

    private async Task ArrangeSales(int productId)
    {
        var date = DateTime.Parse("09/19/2023");
        await _databaseService.CreateSale(productId, date, 10, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 15, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 30, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 50, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 0, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 0, 50, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 0, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 0, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 50, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 30, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 40, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 15, 100, CancellationToken.None);
        date = date.AddDays(-1);
        await _databaseService.CreateSale(productId, date, 29, 100, CancellationToken.None);
    }

    private async Task ArrangeSeasonalCoefficients(int productId)
    {
        var month = 9;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month = 10;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 10m, CancellationToken.None);
    }
}