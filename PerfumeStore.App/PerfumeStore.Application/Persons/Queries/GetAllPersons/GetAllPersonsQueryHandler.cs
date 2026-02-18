using AutoMapper;
using MediatR;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Queries.GetAllPersons {
    public class GetAllPersonsQueryHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonDto>> {
        public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken) {
            var persons = await personsRepository.GetAllAsync(request.Search);
            var personDtos = mapper.Map<IEnumerable<PersonDto>>(persons);
            return personDtos;
        }
    }
}
