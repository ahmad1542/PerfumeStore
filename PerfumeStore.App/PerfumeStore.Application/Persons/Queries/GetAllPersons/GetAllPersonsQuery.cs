using MediatR;
using PerfumeStore.Application.Persons.Dtos;

namespace PerfumeStore.Application.Persons.Queries.GetAllPersons {
    public class GetAllPersonsQuery(string? search) : IRequest<IEnumerable<PersonDto>> {
        public string? Search { get; set; } = search;
    }
}
