namespace ProductPlanningPresentation.Models;

public record CalculateDemandSuppliedRequest(int ProductId, int Days, DateTime Date);