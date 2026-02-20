using MediatR;

namespace PerfumeStore.Application.Persons.Commands.CreatePerson {
    public class CreatePersonCommand : IRequest<Guid> {
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
