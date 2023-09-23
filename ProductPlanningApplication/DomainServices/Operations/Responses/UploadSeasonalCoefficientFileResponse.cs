using ProductPlanningApplication.Dtos;

namespace ProductPlanningApplication.DomainServices.Operations.Responses;

public record struct UploadSeasonalCoefficientFileResponse(List<SeasonalCoefficientDto> Sales);
