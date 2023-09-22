using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CalculateDemandOperation
{
    public record struct Request(int ProductId, int Days) : IRequest<Response>;

    public record struct Response(CalculateDemandDto Demand);
}