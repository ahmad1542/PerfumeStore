using MediatR;

namespace PerfumeStore.Application.Persons.Commands.UpdatePerson {
    public class UpdatePersonCommand(Guid id) : IRequest {
        public Guid Id { get; set; } = id;
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
