using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CalculateSalesPredictionOperation
{
    public record struct Request(int ProductId, int Days) : IRequest<Response>;

    public record struct Response(CalculateSalesPredictionDto SalesPrediction);
}