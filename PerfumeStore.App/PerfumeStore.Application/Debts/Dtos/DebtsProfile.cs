using AutoMapper;
using PerfumeStore.Application.Debts.Commands.CreateDebt;
using PerfumeStore.Application.Debts.Commands.UpdateDebt;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Dtos {
    public class DebtsProfile : Profile {
        public DebtsProfile() {
            CreateMap<Debt, DebtDto>()
                .ForMember(d => d.PersonPhone, opt => opt.MapFrom(s => s.Person != null ? s.Person.Phone : null))
                .ForMember(d => d.PersonName, opt => opt.MapFrom(s => s.Person != null ? s.Person.Name : null))
                .ForMember(d => d.PartyType, opt => opt.MapFrom(s =>
                    s.Person != null
                        ? (s.Person.GetType().Name == "Supplier" ? "Supplier" :
                           s.Person.GetType().Name == "Customer" ? "Customer" : "Person")
                        : s.SalesInvoiceId.HasValue ? "Sales Invoice"
                        : s.PurchaseInvoiceId.HasValue ? "Purchase Invoice"
                        : null));

            CreateMap<CreateDebtCommand, Debt>();
            CreateMap<UpdateDebtCommand, Debt>();
        }
    }
}
