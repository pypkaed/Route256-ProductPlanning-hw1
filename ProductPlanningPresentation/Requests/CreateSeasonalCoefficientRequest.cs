namespace ProductPlanningPresentation.Requests;

public record CreateSeasonalCoefficientRequest(
        int ProductId,
        decimal Coefficient,
        int Month);