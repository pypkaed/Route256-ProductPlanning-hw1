using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CreateSeasonalCoefficientOperation
{
    public record struct Request(
        int ProductId,
        decimal Coefficient,
        int Month) 
        : IRequest<Response>;

    public record struct Response(SeasonalCoefficientDto SeasonalCoefficient);
}