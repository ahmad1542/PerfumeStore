using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Customers.Commands.CreateCustomer;
using PerfumeStore.Application.Customers.Commands.UpdateCustomer;
using PerfumeStore.Application.Customers.Dtos;
using PerfumeStore.Application.Customers.Queries.GetAllCustomers;
using PerfumeStore.Application.Customers.Queries.GetCustomerById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto?>> GetById([FromRoute] Guid id) {
            var customer = await mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll([FromQuery] string? search) {
            var customers = await mediator.Send(new GetAllCustomersQuery(search));
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command) {
            Guid id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerCommand command) {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
