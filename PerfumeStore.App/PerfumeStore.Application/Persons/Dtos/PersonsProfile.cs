using AutoMapper;
using PerfumeStore.Application.Persons.Commands.CreatePerson;
using PerfumeStore.Application.Persons.Commands.UpdatePerson;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Persons.Dtos {
    public class PersonsProfile : Profile {
        public PersonsProfile() {
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.TotalDebt, opt => opt.MapFrom(s => s.Debts.Sum(x => x.Amount)));
            CreateMap<Person, CreatePersonCommand>();
            CreateMap<Person, UpdatePersonCommand>();
        }
    }
}
