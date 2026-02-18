using AutoMapper;
using MediatR;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo {
    public class GetPersonByPhoneNoQueryHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<GetPersonByPhoneNoQuery, PersonDto> {
        public async Task<PersonDto> Handle(GetPersonByPhoneNoQuery request, CancellationToken cancellationToken) {
            var person = await personsRepository.GetByPhoneNoAsync(request.Phone);
            if (person is null)
                throw new NotFoundException(nameof(Person), request.Phone);

            var personDto = mapper.Map<PersonDto>(person);
            return personDto;
        }
    }
}
