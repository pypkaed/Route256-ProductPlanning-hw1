using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Sales;

public static class CreateSaleOperation
{
    public record struct Request(int ProductId, DateTime Date, decimal Sales, decimal Stock) : IRequest<Response>;

    public record struct Response(SaleDto Sale);
}