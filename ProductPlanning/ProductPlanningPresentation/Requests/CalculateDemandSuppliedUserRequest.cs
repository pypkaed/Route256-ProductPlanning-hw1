namespace ProductPlanningPresentation.Requests;

public record CalculateDemandSuppliedUserRequest(int ProductId, int Days, DateOnly Date);