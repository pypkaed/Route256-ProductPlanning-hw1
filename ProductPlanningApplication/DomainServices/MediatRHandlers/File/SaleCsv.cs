namespace ProductPlanningApplication.DomainServices.MediatRHandlers.File;

public class SaleCsv
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Sales { get; set; }
    public decimal Stock { get; set; }
}