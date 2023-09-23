namespace ProductPlanningApplication.Dtos.Csv;

public class SaleCsv
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Sales { get; set; }
    public decimal Stock { get; set; }
}