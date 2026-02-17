using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Queries.GetAllPersons {
    public class GetAllPersonsCommandHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<GetAllPersonsCommand, IEnumerable<Person>> {
        public async Task<IEnumerable<Person>> Handle(GetAllPersonsCommand request, CancellationToken cancellationToken) {
            
        }
    }
}
