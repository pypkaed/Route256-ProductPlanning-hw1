using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public class CalculateSalesPredictionOperation
{
    public record struct Request(Guid ProductId, int Days) : IRequest<Response>;

    public record struct Response(decimal SalesPrediction);
}