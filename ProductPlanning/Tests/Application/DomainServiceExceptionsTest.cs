using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices;
using ProductPlanningApplication.DomainServices.Services;
using ProductPlanningApplication.DomainServices.Services.Interfaces;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDataAccess;
using Xunit;


namespace Tests.Application;

[Collection("NonParallel")]
public class DomainServiceExceptionsTest : IDisposable
{
    private readonly ProductPlanningDatabaseContext _context;
    private readonly IDatabaseService _databaseService;
    private readonly IProductPlanningCalculator _calculator;

    public DomainServiceExceptionsTest()
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
    public async Task TryGetEntity_ThrowServiceException()
    {
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            await _calculator.CalculateSalesPrediction(1, 15, CancellationToken.None);
        });
    }

    [Fact]
    public async Task CreateSale_ThrowServiceException()
    {
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            var dateNow = DateOnly.FromDateTime(DateTime.Now);

            await _databaseService.CreateSale(1, dateNow, 10, 50, CancellationToken.None);
            await _databaseService.CreateSale(1, dateNow, 50, 100, CancellationToken.None);
        });
    }

    [Fact]
    public async Task CalculateAds_ThrowCalculationsException()
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        await _databaseService.CreateSale(1, dateNow, 0, 0, CancellationToken.None);
        await _databaseService.CreateSale(1, dateNow.AddDays(1), 0, 0, CancellationToken.None);

        await Assert.ThrowsAsync<CalculationsException>(async () =>
        {
            await _calculator.CalculateAverageDailySales(1, CancellationToken.None);
        });
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}