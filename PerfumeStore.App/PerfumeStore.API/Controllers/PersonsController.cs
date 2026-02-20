using MediatR;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Application.Persons.Commands.CreatePerson;
using PerfumeStore.Application.Persons.Commands.UpdatePerson;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Application.Persons.Queries.GetAllPersons;
using PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo;

namespace PerfumeStore.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController(IMediator mediator) : ControllerBase {

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto?>> GetById([FromRoute] Guid id) {
            var person = await mediator.Send(new GetPersonByIdQuery(id));

            return Ok(person);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] string? search) {
            var persons = await mediator.Send(new GetAllPersonsQuery(search));
            return Ok(persons);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command) {
            Guid id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] Guid id, [FromBody] UpdatePersonCommand command) {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

    }
}
