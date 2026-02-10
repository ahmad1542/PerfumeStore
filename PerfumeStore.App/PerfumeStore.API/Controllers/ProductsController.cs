using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Products.Commands.CreateProduct;
using PerfumeStore.Application.Products.Commands.UpdateProduct;
using PerfumeStore.Application.Products.Dtos;
using PerfumeStore.Application.Products.Queries.GetAllProducts;
using PerfumeStore.Application.Products.Queries.GetProductById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetById([FromRoute] int id) {
            var product = await mediator.Send(new GetProductByIdQuery(id));

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] string? search) {
            var products = await mediator.Send(new GetAllProductsQuery(search));
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateProductCommand command) {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] int id, [FromBody] UpdateProductCommand command) {
            command.ID = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
