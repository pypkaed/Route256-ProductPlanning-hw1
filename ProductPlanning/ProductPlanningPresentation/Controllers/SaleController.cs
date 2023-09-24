using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductPlanningApplication.DomainServices.Operations.Requests;
using ProductPlanningApplication.Dtos;
using ProductPlanningPresentation.Requests;

namespace ProductPlanningPresentation.Controllers;

[ApiController]
[Route("api/")]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create-sale")]
    public async Task<ActionResult<SaleDto>> CreateSale(
        [FromBody] CreateSaleUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var sendRequest = new CreateSaleRequest(
            userRequest.ProductId,
            userRequest.Date,
            userRequest.Sales,
            userRequest.Stock);
        var response = await _mediator.Send(sendRequest, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("upload-file-sales")]
    public async Task<ActionResult<List<SaleDto>>> CreateSaleEntriesFromFile(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var request = new UploadSalesFileRequest(file.OpenReadStream());
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}