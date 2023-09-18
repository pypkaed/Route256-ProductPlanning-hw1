namespace ProductPlanningPresentation.Models;

public record CreateSeasonalCoefficientModel(
        int ProductId,
        decimal Coefficient,
        int Month);