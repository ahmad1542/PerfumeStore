using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Commands.CreatePerson {
    public class CreatePersonCommandHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<CreatePersonCommand, string> {
        public async Task<string> Handle(CreatePersonCommand request, CancellationToken cancellationToken) {
            var person = mapper.Map<Person>(request);
            string phoneNo = await personsRepository.AddAsync(person);
            return phoneNo;
        }
    }
}
