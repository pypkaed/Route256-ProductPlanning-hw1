using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using ProductPlanningApplication.Dtos;
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
            var coefficient = new List<SeasonalCoefficientDto>();

            while (await csv.ReadAsync())
            {
                var coef = csv.GetRecord<SeasonalCoefficientCsv>();

                coefficient.Add(await _databaseService.CreateSeasonalCoefficient(
                    coef.Id, coef.Month, coef.Coeff, cancellationToken));
            }
            
            return new Response(coefficient);
        }
    }
}