using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using ProductPlanningApplication.Dtos.Mapping;
using static ProductPlanningApplication.DomainServices.MediatROperations.Files.UploadSeasonalCoefficientFileOperation;

namespace ProductPlanningApplication.DomainServices.MediatRHandlers.File;

public class UploadSeasonalCoefficientFileHandler : IRequestHandler<Request, Response>
{
    private readonly IDatabaseService _databaseService;

    public UploadSeasonalCoefficientFileHandler(IDatabaseService databaseService)
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
            var seasonalCoefficients = csv.GetRecords<SeasonalCoefficientCsv>().AsSeasonalCoefficient();

            var coefficientsDto = await _databaseService.CreateSeasonalCoefficientsBulk(seasonalCoefficients, cancellationToken);
            
            return new Response(coefficientsDto);
        }
    }
}