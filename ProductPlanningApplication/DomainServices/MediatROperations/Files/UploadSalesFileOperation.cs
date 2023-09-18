using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Files;

public static class UploadSalesFileOperation
{
    public record struct Request(Stream FileStream) : IRequest<Response>;

    public record struct Response(List<SaleDto> Sales);
}