using AutoMapper;
using PerfumeStore.Application.Customers.Commands.CreateCustomer;
using PerfumeStore.Application.Customers.Commands.UpdateCustomer;
using PerfumeStore.Application.Persons.Dtos;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Customers.Dtos {
    public class CustomersProfile : Profile {
        public CustomersProfile() {
            CreateMap<Customer, CustomerDto>()
                .IncludeBase<Person, PersonDto>()
                .ForMember(d => d.TotalSalesInvoices, opt => opt.MapFrom(s => s.SalesInvoices.Count))
                .ForMember(d => d.TotalDebt, opt => opt.MapFrom(s => s.Debts.Sum(x => x.Amount)));
            CreateMap<Customer, CreateCustomerCommand>();
            CreateMap<Customer, UpdateCustomerCommand>();
        }
    }
}
