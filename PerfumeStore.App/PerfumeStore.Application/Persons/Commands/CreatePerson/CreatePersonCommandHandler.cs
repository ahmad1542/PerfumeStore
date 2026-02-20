using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Commands.CreatePerson {
    public class CreatePersonCommandHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<CreatePersonCommand, Guid> {
        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken) {
            var person = mapper.Map<Person>(request);
            Guid id = await personsRepository.AddAsync(person);
            return id;
        }
    }
}
