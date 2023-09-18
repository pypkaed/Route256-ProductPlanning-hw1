using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Products;

public static class CalculateAverageDailySalesOperation
{
    public record struct Request(Guid ProductId) : IRequest<Response>;

    public record struct Response(decimal AverageDailySales);
}