using MediatR;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Products;

public static class CreateProductOperation
{
    public record struct Request() : IRequest<Response>;

    public record struct Response();
}