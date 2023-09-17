using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public class CreateSeasonalCoefficientHandler
{
    public record struct Request() : IRequest<Response>;

    public record struct Response();
}