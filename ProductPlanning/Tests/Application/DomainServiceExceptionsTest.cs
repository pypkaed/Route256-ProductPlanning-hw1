using Microsoft.EntityFrameworkCore;
using ProductPlanningApplication.DataAccess;
using ProductPlanningApplication.DomainServices;
using ProductPlanningApplication.DomainServices.Services;
using ProductPlanningApplication.DomainServices.Services.Interfaces;
using ProductPlanningApplication.Exceptions;
using ProductPlanningDataAccess;
using Xunit;


namespace Tests.Application;

public class DomainServiceExceptionsTest
{
    private readonly IDatabaseService _databaseService;
    private readonly IProductPlanningCalculator _calculator;

    public DomainServiceExceptionsTest()
    {
        var options =
            new DbContextOptionsBuilder<ProductPlanningDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        var context = new ProductPlanningDatabaseContext(options);
        context.Database.EnsureCreated();
        context.Database.EnsureDeleted();

        _databaseService = new DatabaseService(context);
        _calculator = new ProductPlanningCalculator(context);
    }

    [Fact]
    public async Task TryGetEntity_ThrowServiceException()
    {
        await Assert.ThrowsAsync<ServiceException>(async () =>
        {
            await _calculator.CalculateSalesPredictionAsync(1, 15, CancellationToken.None);
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
            await _calculator.CalculateAverageDailySalesAsync(1, CancellationToken.None);
        });
    }
}