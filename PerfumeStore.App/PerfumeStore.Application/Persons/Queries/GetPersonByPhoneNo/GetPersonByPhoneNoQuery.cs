using MediatR;
using PerfumeStore.Application.Persons.Dtos;

namespace PerfumeStore.Application.Persons.Queries.GetPersonByPhoneNo {
    public class GetPersonByPhoneNoQuery(string phone) : IRequest<PersonDto> {
        public string Phone { get; set; } = phone;
    }
}
