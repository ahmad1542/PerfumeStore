using AutoMapper;
using MediatR;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo {
    public class GetPersonByIdQueryHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<GetPersonByIdQuery, PersonDto> {
        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken) {
            var person = await personsRepository.GetByIdAsync(request.Id);
            if (person is null)
                throw new NotFoundException(nameof(Person), request.Id.ToString());

            var personDto = mapper.Map<PersonDto>(person);
            return personDto;
        }
    }
}
