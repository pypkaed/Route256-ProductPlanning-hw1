using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.DomainServices.Operations.Responses;
using ProductPlanningApplication.Dtos.Csv;
using ProductPlanningApplication.Dtos.Mapping;

namespace ProductPlanningApplication.DomainServices.Handlers.File;

public class UploadSeasonalCoefficientFileHandler 
    : IRequestHandler<UploadSeasonalCoefficientFileRequest, UploadSeasonalCoefficientFileResponse>
{
    private readonly IDatabaseService _databaseService;

    public UploadSeasonalCoefficientFileHandler(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    
    public async Task<UploadSeasonalCoefficientFileResponse> Handle(
        UploadSeasonalCoefficientFileRequest request,
        CancellationToken cancellationToken)
    {
        if (request.FileStream.Length <= 0)
            throw new Exception("Empty file");

        
        using (var reader = new StreamReader(request.FileStream))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var seasonalCoefficients = csv.GetRecords<SeasonalCoefficientCsv>().AsSeasonalCoefficient();

            var coefficientsDto = await _databaseService.CreateSeasonalCoefficientsBulk(seasonalCoefficients, cancellationToken);
            
            return new UploadSeasonalCoefficientFileResponse(coefficientsDto);
        }
    }
}