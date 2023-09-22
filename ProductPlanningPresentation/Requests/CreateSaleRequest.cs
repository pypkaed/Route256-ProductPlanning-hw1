namespace ProductPlanningPresentation.Requests;

public record CreateSaleRequest(
    int ProductId,
    DateOnly Date, 
    decimal Sales, 
    decimal Stock);