using ProductPlanningDomain.Exceptions;
using ProductPlanningDomain.Sales;
using ProductPlanningDomain.Sales.ValueObjects;
using Xunit;

namespace Tests.Domain;

public class SalesTest
{
    [Theory]
    [InlineData(-10, 10, 50)]
    [InlineData(1, 50, 10)]
    [InlineData(1, -10, 10)]
    public void CreateSale_ThrowValueObjectException(
        int productId,
        decimal amountSold,
        decimal inStock)
    {
        Assert.Throws<ValueObjectException>(() =>
        {
            var sales = new ProductAmount(amountSold);
            var stock = new ProductAmount(inStock);
            var sale = new Sale(productId, DateTime.Now, sales, stock);
        });
    }
    
    [Theory]
    [InlineData(-10, 1.5, 9)]
    [InlineData(10, -1.5, 9)]
    [InlineData(10, 11.5, 9)]
    [InlineData(10, 1.5, 15)]
    [InlineData(10, 1.5, -15)]
    public void CreateSeasonalCoefficient_ThrowValueObjectException(
        int productId,
        decimal coefficient,
        int month)
    {
        Assert.Throws<ValueObjectException>(() =>
        {
            var coeff = new Coefficient(coefficient);
            var seasonalCoefficient = new SeasonalCoefficient(productId, coeff, month);
        });
    }
}