namespace ProductPlanningPresentation.Requests;

public record CalculateDemandSuppliedRequest(int ProductId, int Days, DateTime Date);