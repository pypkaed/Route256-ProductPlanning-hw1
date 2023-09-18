using MediatR;
using ProductPlanningApplication.DomainServices.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Products;

public static class CreateProductOperation
{
    public record struct Request(int Stock) : IRequest<Response>;

    public record struct Response(ProductDto ProductDto);
}