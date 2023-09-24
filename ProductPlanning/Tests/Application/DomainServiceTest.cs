using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DomainServices.Services;
using ProductPlanningApplication.DomainServices.Services.Interfaces;
using ProductPlanningDataAccess;
using Xunit;

namespace Tests.Application;

[Collection("NonParallel")]
public class DomainServiceTest : IDisposable
{
    private readonly ProductPlanningDatabaseContext _context;
    private readonly IDatabaseService _databaseService;
    private readonly IProductPlanningCalculator _calculator;

    public DomainServiceTest()
    {
        var options =
            new DbContextOptionsBuilder<ProductPlanningDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        _context = new ProductPlanningDatabaseContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _databaseService = new DatabaseService(_context);
        _calculator = new ProductPlanningCalculator(_context);
    }

    [Fact]
    public async Task CalculateAds()
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);

        await _databaseService.CreateSale(
            productId: 1,
            dateNow,
            sales: 10,
            stock: 50,
            CancellationToken.None);
        await _databaseService.CreateSale(
            productId: 1,
            dateNow.AddDays(1),
            sales: 5, 
            stock: 40, 
            CancellationToken.None);

        var ads = await _calculator.CalculateAverageDailySales(1, CancellationToken.None);
        
        Assert.Equal(7.5m, ads.AverageDailySales);
    }
    
    [Fact]
    public async Task CalculateSalesPrediction()
    {
        // 12 days in September with seasonal coefficient of 0.1,
        // 1 day in November with seasonal coefficient 10
        await ArrangeSales(productId: 1);
        await ArrangeSeasonalCoefficients(productId: 1);

        var ads = await _calculator.CalculateAverageDailySales(productId: 1, CancellationToken.None);
        var salesPrediction = await _calculator.CalculateSalesPrediction(
            productId: 1,
            numOfDays: 13,
            currentDate: DateOnly.Parse("09/19/2023"),
            CancellationToken.None);
        
        Assert.Equal(ads.AverageDailySales * ((12m * 0.1m) + (1m * 10m)), salesPrediction.SalesPrediction);
    }

    [Fact]
    public async Task CalculateDemandNow()
    {
        await ArrangeSales(productId: 1);
        await ArrangeSeasonalCoefficients(productId: 1);
        
        var salesPrediction = await _calculator.CalculateSalesPrediction(
            productId: 1,
            numOfDays: 13,
            CancellationToken.None);
        var demand = await _calculator.CalculateDemand(
            productId: 1,
            numOfDays: 13,
            CancellationToken.None);
        var expected = salesPrediction.SalesPrediction - 100;
        expected = expected > 0 ? expected : 0;
        
        Assert.Equal((float) expected, (float) demand.Demand);
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

        var dateNow = DateOnly.FromDateTime(DateTime.Now);

        var supplySalesPrediction = await _calculator.CalculateSalesPrediction(
            productId: 1,
            numOfDays: 5,
            CancellationToken.None);
        var stockSupplied = 100 - supplySalesPrediction.SalesPrediction;
        var salesPrediction = await _calculator.CalculateSalesPrediction(
            productId: 1,
            numOfDays: 13 - 5,
            currentDate: dateNow.AddDays(5),
            CancellationToken.None);
        
        var demand = await _calculator.CalculateDemandSupplied(
            productId: 1,
            numOfDays: 13,
            supplyDate: DateOnly.Parse("09/25/2023"),
            CancellationToken.None);

        var expected = salesPrediction.SalesPrediction - stockSupplied;
        
        Assert.Equal((float)expected,(float) demand.Demand);
    }

    private async Task ArrangeSales(int productId)
    {
        var date = DateOnly.Parse("09/19/2023");
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
        var month = 1;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 0.1m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 10m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 10m, CancellationToken.None);
        month++;
        await _databaseService.CreateSeasonalCoefficient(productId, month, 10m, CancellationToken.None);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}