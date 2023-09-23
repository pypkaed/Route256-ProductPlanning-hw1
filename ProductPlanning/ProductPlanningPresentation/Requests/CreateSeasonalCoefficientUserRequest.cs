namespace ProductPlanningPresentation.Requests;

public record CreateSeasonalCoefficientUserRequest(
        int ProductId,
        decimal Coefficient,
        int Month);