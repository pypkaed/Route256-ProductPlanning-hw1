using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CalculateAverageDailySalesOperation
{
    public record struct Request(int ProductId) : IRequest<Response>;

    public record struct Response(CalculateAverageDailySalesDto AverageDailySales);
}