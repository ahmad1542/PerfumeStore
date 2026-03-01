using AutoMapper;
using MediatR;
using PerfumeStore.Application.MoneyTransactions.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyTransactions.Queries.GetMoneyTransactionById {
    public class GetMoneyTransactionByIdQueryHandler(IMoneyTransactionsRepository moneyTransactionsRepository, IMapper mapper) : IRequestHandler<GetMoneyTransactionByIdQuery, MoneyTransactionDto> {
        public async Task<MoneyTransactionDto> Handle(GetMoneyTransactionByIdQuery request, CancellationToken cancellationToken) {
            var moneyTransaction = await moneyTransactionsRepository.GetByIdAsync(request.ID);
            if (moneyTransaction is null)
                throw new NotFoundException(nameof(MoneyTransaction), request.ID.ToString());
            return mapper.Map<MoneyTransactionDto>(moneyTransaction);
        }
    }
}
