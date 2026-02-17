using MediatR;

namespace PerfumeStore.Application.Persons.Commands.UpdatePerson {
    public class UpdatePersonCommand(string phone) : IRequest {
        public string Phone { get; set; } = phone;
        public string Name { get; set; } = default!;
    }
}
