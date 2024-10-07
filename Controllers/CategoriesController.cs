using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.APP.CQRS.Categories.Add;
using Shop.APP.CQRS.Categories.Delete;
using Shop.APP.CQRS.Categories.Queries.GetCategories;
using Shop.APP.CQRS.Categories.Queries.GetCategoryById;
using Shop.APP.CQRS.Categories.Queries.GetCategoryProducts;
using System.Data;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDbConnection _dbConnection;

        public CategoriesController(IMediator mediator, IDbConnection dbConnection)
        {
            _mediator = mediator;
            _dbConnection = dbConnection;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddNewCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var command = new GetCategoryByIdCommand
            {
                Id = id
            };

            var result =await _mediator.Send(command);

            return Ok(result); 
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var command = new GetCategoriesCommand
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByCategoryId([FromRoute]int id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var command = new GetCategoryProductsCommand
            {
                Id = id,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
