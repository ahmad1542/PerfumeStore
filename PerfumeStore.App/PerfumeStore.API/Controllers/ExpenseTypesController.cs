using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.ExpenseTypes.Commands.CreateExpenseType;
using PerfumeStore.Application.ExpenseTypes.Commands.UpdateExpenseType;
using PerfumeStore.Application.ExpenseTypes.Dtos;
using PerfumeStore.Application.ExpenseTypes.Queries.GetAllExpenseTypes;
using PerfumeStore.Application.ExpenseTypes.Queries.GetExpenseTypeById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseTypeDto?>> GetById([FromRoute] int id) {
            var expenseType = await mediator.Send(new GetExpenseTypeByIdQuery(id));
            return Ok(expenseType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseTypeDto>>> GetAll([FromQuery] string? search) {
            var expenseTypes = await mediator.Send(new GetAllExpenseTypesQuery(search));
            return Ok(expenseTypes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseTypeCommand command) {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateExpenseTypeCommand command) {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
