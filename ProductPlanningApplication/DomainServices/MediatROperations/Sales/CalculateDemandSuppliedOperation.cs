using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CalculateDemandSuppliedOperation
{
    public record struct Request(int ProductId, int Days, DateTime Date) : IRequest<Response>;

    public record struct Response(CalculateDemandSuppliedDto Demand);
}