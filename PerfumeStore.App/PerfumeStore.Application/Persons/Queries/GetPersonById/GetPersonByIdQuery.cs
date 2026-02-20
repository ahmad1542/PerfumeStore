using MediatR;
using PerfumeStore.Application.Persons.Dtos;

namespace PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo {
    public class GetPersonByIdQuery(Guid id) : IRequest<PersonDto> {
        public Guid Id { get; set; } = id;
    }
}
