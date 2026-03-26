using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Commands.UpdateSalesInvoice {
    public class UpdateSalesInvoiceCommandHandler(ISalesInvoicesRepository salesInvoicesRepository, IDebtsRepository debtsRepository, IMoneyAccountsRepository moneyAccountsRepository) : IRequestHandler<UpdateSalesInvoiceCommand> {
        public async Task Handle(UpdateSalesInvoiceCommand request, CancellationToken cancellationToken) {

            var salesInvoice = await salesInvoicesRepository.GetByIdAsync(request.ID);

            if (salesInvoice == null)
                throw new NotFoundException(nameof(SalesInvoice), request.ID.ToString());

            var oldAmountPaid = salesInvoice.AmountPaid;
            var oldMoneyAccountId = salesInvoice.MoneyAccountId;

            MoneyAccount? oldAccount = null;
            MoneyAccount? newAccount = null;

            if (request.AmountPaid > 0) {
                if (!request.MoneyAccountId.HasValue)
                    throw new Exception("Money account is required when amount is paid.");

                newAccount = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountId.Value);

                if (newAccount == null)
                    throw new Exception("Invalid money account.");
            } else if (request.MoneyAccountId.HasValue) {
                throw new Exception("Money account should only be selected when amount paid is greater than zero.");
            }

            if (oldAmountPaid > 0 && oldMoneyAccountId.HasValue) {
                oldAccount = await moneyAccountsRepository.GetByIdAsync(oldMoneyAccountId.Value);

                if (oldAccount == null)
                    throw new Exception("Old money account not found.");
            }

            if (oldAccount != null) {
                oldAccount.CurrentBalance -= oldAmountPaid;
            }

            salesInvoice.Date = request.Date;
            salesInvoice.Notes = request.Notes;
            salesInvoice.CustomerId = request.CustomerId;
            salesInvoice.AmountPaid = request.AmountPaid;
            salesInvoice.MoneyAccountId = request.AmountPaid > 0 ? request.MoneyAccountId : null;

            if (newAccount != null) {
                newAccount.CurrentBalance += request.AmountPaid;
            }

            if (request.HasDebt && request.DebtAmount.HasValue && request.DebtAmount.Value > 0) {
                if (salesInvoice.Debt != null) {
                    salesInvoice.Debt.Amount = request.DebtAmount.Value;
                    salesInvoice.Debt.Notes = request.DebtNotes;
                    salesInvoice.Debt.PersonId = request.CustomerId;
                    salesInvoice.Debt.SalesInvoiceId = salesInvoice.ID;
                } else {
                    salesInvoice.Debt = new Debt {
                        Amount = request.DebtAmount.Value,
                        Notes = request.DebtNotes,
                        PersonId = request.CustomerId,
                        SalesInvoiceId = salesInvoice.ID
                    };
                }
            } else {
                if (salesInvoice.Debt != null) {
                    await debtsRepository.SoftDeleteAsync(salesInvoice.Debt);
                    salesInvoice.Debt = null;
                }
            }

            await salesInvoicesRepository.UpdateProductsAsync(salesInvoice.ID, request.Products);
        }
    }
}
