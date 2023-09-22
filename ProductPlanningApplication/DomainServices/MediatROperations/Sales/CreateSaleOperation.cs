using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CreateSaleOperation
{
    public record struct Request(int ProductId, DateOnly Date, decimal Sales, decimal Stock) : IRequest<Response>;

    public record struct Response(SaleDto Sale);
}