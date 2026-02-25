using AutoMapper;
using PerfumeStore.Application.MoneyAccounts.Commands.CreateMoneyAccount;
using PerfumeStore.Application.MoneyAccounts.Commands.UpdateMoneyAccount;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.MoneyAccounts.Dtos {
    public class MoneyAccountsProfile : Profile {
        public MoneyAccountsProfile() {
            CreateMap<MoneyAccount, MoneyAccountDto>();
            CreateMap<CreateMoneyAccountCommand, MoneyAccount>();
            CreateMap<UpdateMoneyAccountCommand, MoneyAccount>();
        }
    }
}
