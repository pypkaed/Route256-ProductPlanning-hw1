using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public class CalculateSalesPredictionOperation
{
    public record struct Request() : IRequest<Response>;

    public record struct Response();
}