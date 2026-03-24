using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice {
    public class CreateSalesInvoiceCommandHandler(ISalesInvoicesRepository salesInvoicesRepository, IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<CreateSalesInvoiceCommand, long> {
        public async Task<long> Handle(CreateSalesInvoiceCommand request, CancellationToken cancellationToken) {
            var salesInvoice = mapper.Map<SalesInvoice>(request);

            MoneyAccount? account = null;

            if (request.AmountPaid > 0) {
                if (!request.MoneyAccountId.HasValue)
                    throw new Exception("Money account is required when amount is paid.");

                account = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountId.Value);

                if (account == null)
                    throw new Exception("Invalid money account.");
            }

            await salesInvoicesRepository.AddAsync(salesInvoice, request.Products);

            if (request.AmountPaid > 0) {
                account!.CurrentBalance += request.AmountPaid;
                await moneyAccountsRepository.SaveChangesAsync();
            }

            // await debtsRepository.AddAsync(salesInvoice.Debt!);
            return salesInvoice.ID;
        }
    }
}
