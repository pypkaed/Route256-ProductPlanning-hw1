using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Requests;

namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api/")]
public class SeasonalCoefficientController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeasonalCoefficientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create-seasonal-coefficient")]
    public async Task<ActionResult<SeasonalCoefficientDto>> CreateSeasonalCoefficient(
        [FromBody] CreateSeasonalCoefficientUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CreateSeasonalCoefficientRequest(
            userRequest.ProductId,
            userRequest.Coefficient,
            userRequest.Month);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("upload-file-seasonal-coefficients")]
    public async Task<ActionResult<List<SaleDto>>> CreateSeasonalCoefficientEntriesFromFile(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var request = new UploadSeasonalCoefficientFileRequest(file.OpenReadStream());
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}