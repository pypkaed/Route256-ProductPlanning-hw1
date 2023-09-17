using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public class CalculateDemandOperation
{
    public record struct Request() : IRequest<Response>;

    public record struct Response();
}