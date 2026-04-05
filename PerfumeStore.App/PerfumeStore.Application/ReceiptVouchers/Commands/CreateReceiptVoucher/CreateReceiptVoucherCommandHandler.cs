using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Commands.CreateReceiptVoucher;

public class CreateReceiptVoucherCommandHandler(
    IReceiptVouchersRepository receiptVouchersRepository,
    IMoneyAccountsRepository moneyAccountsRepository,
    ICustomersRepository customersRepository,
    IPersonsRepository personsRepository,
    IDebtsRepository debtsRepository,
    IMapper mapper) : IRequestHandler<CreateReceiptVoucherCommand, long> {

    public async Task<long> Handle(CreateReceiptVoucherCommand request, CancellationToken cancellationToken) {
        if (request.ReceiptForType == "person") {
            var debt = await debtsRepository.GetByIdAsync(request.DebtId!.Value);

            if (debt == null)
                throw new NotFoundException(nameof(Debt), request.DebtId.Value.ToString());

            if (debt.IsDeleted)
                throw new Exception("Selected debt is deleted.");

            if (debt.PersonId != request.PersonId)
                throw new Exception("Selected debt does not belong to the selected person.");

            if (debt.Amount <= 0)
                throw new Exception("Selected debt has no remaining amount.");

            if (request.Amount > debt.Amount)
                throw new Exception("Receipt amount cannot exceed the remaining debt amount.");
        }

        if (request.ReceiptForType == "customer") {
            var customer = await customersRepository.GetByIdAsync(request.CustomerId!.Value);
            if (customer == null) {
                throw new Exception("Selected customer was not found.");
            }
        } else {
            var person = await personsRepository.GetByIdAsync(request.PersonId!.Value);
            if (person == null) {
                throw new Exception("Selected person was not found.");
            }
        }

        var account = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountID);
        if (account == null) {
            throw new Exception("Selected money account was not found.");
        }

        var receiptVoucher = mapper.Map<ReceiptVoucher>(request);
        var salesApplications = mapper.Map<List<ReceiptVoucherSalesInvoice>>(request.SalesApplications);

        var id = await receiptVouchersRepository.AddAsync(receiptVoucher, salesApplications, request.DebtId);

        account.CurrentBalance += request.Amount;
        await moneyAccountsRepository.SaveChangesAsync();

        return id;
    }
}
