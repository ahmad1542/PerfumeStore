using AutoMapper;
using PerfumeStore.Application.MoneyTransactions.Commands.CreateMoneyTransaction;
using PerfumeStore.Application.MoneyTransactions.Commands.UpdateMoneyTransaction;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.MoneyTransactions.Dtos {
    public class MoneyTransactionsProfile : Profile {
        public MoneyTransactionsProfile() {
            CreateMap<MoneyTransaction, MoneyTransactionDto>()
                .ForMember(dest => dest.FromMoneyAccountName, opt => opt.MapFrom(src => src.FromMoneyAccount.AccountName))
                .ForMember(dest => dest.ToMoneyAccountName, opt => opt.MapFrom(src => src.ToMoneyAccount.AccountName));
            CreateMap<CreateMoneyTransactionCommand, MoneyTransaction>();
            CreateMap<UpdateMoneyTransactionCommand, MoneyTransaction>();
        }
    }
}
