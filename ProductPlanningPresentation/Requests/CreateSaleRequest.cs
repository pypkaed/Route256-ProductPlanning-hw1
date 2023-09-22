namespace ProductPlanningPresentation.Models;

public record CreateSaleRequest(
    int ProductId,
    DateTime Date, 
    decimal Sales, 
    decimal Stock);