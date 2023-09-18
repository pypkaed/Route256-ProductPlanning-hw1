namespace ProductPlanningPresentation.Models;

public record CreateSaleModel(
    int ProductId,
    DateTime Date, 
    decimal Sales, 
    decimal Stock);