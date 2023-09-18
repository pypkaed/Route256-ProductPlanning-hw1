using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CalculateAverageDailySalesOperation
{
    public record struct Request(int ProductId) : IRequest<Response>;

    public record struct Response(decimal AverageDailySales);
}