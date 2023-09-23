namespace ProductPlanningPresentation.Requests;

public record CreateSaleUserRequest(
    int ProductId,
    DateOnly Date, 
    decimal Sales, 
    decimal Stock);