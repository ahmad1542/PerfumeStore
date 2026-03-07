using AutoMapper;
using PerfumeStore.Application.Debts.Commands.CreateDebt;
using PerfumeStore.Application.Debts.Commands.UpdateDebt;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Dtos {
    public class DebtsProfile : Profile {
        public DebtsProfile() {
            CreateMap<Debt, DebtDto>()
                .ForMember(d => d.PersonPhone, opt => opt.MapFrom(s => s.Person != null ? s.Person.Phone : null));
            CreateMap<CreateDebtCommand, Debt>();
            CreateMap<UpdateDebtCommand, Debt>();
        }
    }
}
