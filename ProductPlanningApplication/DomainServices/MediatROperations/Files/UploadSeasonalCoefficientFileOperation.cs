using MediatR;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.MediatROperations.Files;

public static class UploadSeasonalCoefficientFileOperation
{
    public record struct Request(Stream FileStream) : IRequest<Response>;
    public record struct Response(List<SeasonalCoefficientDto> Sales);
}