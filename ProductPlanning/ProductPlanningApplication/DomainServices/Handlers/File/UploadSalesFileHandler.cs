using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;
using ProductPlanningApplication.DomainServices.Services.Interfaces;
using ProductPlanningApplication.Dtos.Csv;
using ProductPlanningApplication.Dtos.Mapping;
using ProductPlanningDomain.Sales;

namespace ProductPlanningApplication.DomainServices.Handlers.File;

public class UploadSalesFileHandler
    : IRequestHandler<UploadSalesFileRequest, UploadSalesFileResponse>
{
    private readonly IDatabaseService _databaseService;

    public UploadSalesFileHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<UploadSalesFileResponse> Handle(
        UploadSalesFileRequest request,
        CancellationToken cancellationToken)
    {
        if (request.FileStream.Length <= 0)
            throw new Exception("Empty file");

        IEnumerable<Sale> sales;
        
        using (var reader = new StreamReader(request.FileStream))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            sales = csv.GetRecords<SaleCsv>().AsSale();
        }

        var salesDto = await _databaseService.CreateSalesBulk(sales, cancellationToken);
            
        return new UploadSalesFileResponse(salesDto);
        
    }
}