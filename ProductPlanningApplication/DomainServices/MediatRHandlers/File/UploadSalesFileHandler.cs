using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using ProductPlanningApplication.Dtos;
using ProductPlanningApplication.Dtos.Mapping;
using static ProductPlanningApplication.DomainServices.MediatROperations.Files.UploadSalesFileOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.File;

public class UploadSalesFileHandler : IRequestHandler<Request, Response>
{
    private readonly IDatabaseService _databaseService;

    public UploadSalesFileHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        if (request.FileStream.Length <= 0)
            throw new Exception("Empty file");

        
        using (var reader = new StreamReader(request.FileStream))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var sales = csv.GetRecords<SaleCsv>().AsSale();

            var salesDto = await _databaseService.CreateSalesBulk(sales, cancellationToken);
            
            return new Response(salesDto);
        }
    }
}