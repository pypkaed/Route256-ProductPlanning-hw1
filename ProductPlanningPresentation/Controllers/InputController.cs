using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.MediatROperations.Files;
using ProductPlanningApplication.Dtos;

namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api/")]
public class InputController : ControllerBase
{
    private readonly IMediator _mediator;

    public InputController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("upload-file-sales")]
    public async Task<ActionResult<List<SaleDto>>> CreateSaleEntriesFromFile(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var request = new UploadSalesFileOperation.Request(file.OpenReadStream());
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response.Sales);
    }
    
    [HttpPost]
    [Route("upload-file-seasonal-coefficients")]
    public async Task<ActionResult<List<SaleDto>>> CreateSeasonalCoefficientEntriesFromFile(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var request = new UploadSeasonalCoefficientFileOperation.Request(file.OpenReadStream());
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response.Sales);
    }
}