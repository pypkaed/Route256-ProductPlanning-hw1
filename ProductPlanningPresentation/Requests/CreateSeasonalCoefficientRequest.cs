namespace ProductPlanningPresentation.Models;

public record CreateSeasonalCoefficientRequest(
        int ProductId,
        decimal Coefficient,
        int Month);