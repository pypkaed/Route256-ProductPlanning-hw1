namespace ProductPlanningApplication.Dtos;

public record SeasonalCoefficientDto(
    int ProductId,
    decimal Coefficient,
    int Month);