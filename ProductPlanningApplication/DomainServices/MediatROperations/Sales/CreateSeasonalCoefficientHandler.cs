using MediatR;
using ProductPlanningApplication.DomainServices.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CreateSeasonalCoefficientHandler
{
    public record struct Request(
        Guid ProductId,
        decimal Coefficient,
        int Month) 
        : IRequest<Response>;

    public record struct Response(SeasonalCoefficientDto SeasonalCoefficient);
}