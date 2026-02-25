using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.MoneyAccounts.Commands.CreateMoneyAccount;
using PerfumeStore.Application.MoneyAccounts.Commands.UpdateMoneyAccount;
using PerfumeStore.Application.MoneyAccounts.Dtos;
using PerfumeStore.Application.MoneyAccounts.Queries.GetAllMoneyAccounts;
using PerfumeStore.Application.MoneyAccounts.Queries.GetMoneyAccountById;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyAccountsController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<MoneyAccountDto?>> GetById([FromRoute] int id) {
            var moneyAccount = await mediator.Send(new GetMoneyAccountByIdQuery(id));

            return Ok(moneyAccount);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneyAccountDto>>> GetAll([FromQuery] string? search) {
            var moneyAccounts = await mediator.Send(new GetAllMoneyAccountsQuery(search));
            return Ok(moneyAccounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoneyAccount([FromBody] CreateMoneyAccountCommand command) {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMoneyAccount([FromRoute] int id, [FromBody] UpdateMoneyAccountCommand command) {
            command.ID = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
