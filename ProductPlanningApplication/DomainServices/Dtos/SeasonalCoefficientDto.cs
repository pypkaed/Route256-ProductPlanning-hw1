namespace ProductPlanningApplication.DomainServices.Dtos;

public record SeasonalCoefficientDto(
    Guid ProductId,
    decimal Coefficient,
    int Month);