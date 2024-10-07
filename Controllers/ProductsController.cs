using Microsoft.AspNetCore.Mvc;
using Shop.APP.CQRS.Products.Add;
using MediatR;
using System.Threading.Tasks;
using Shop.APP.CQRS.Products.Queries.GetProductById;
using Shop.APP.CQRS.Categories.Queries.GetCategories;
using Shop.APP.CQRS.Products.Queries.GetProducts;
using Shop.APP.CQRS.Categories.Delete;
using Shop.APP.CQRS.Products.Delete;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var command = new GetProductByIdCommand
        {
            Id = id
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var command = new GetProductsCommand
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteProductCommand { Id = id };
        await _mediator.Send(command);
        return Ok();
    }
}
