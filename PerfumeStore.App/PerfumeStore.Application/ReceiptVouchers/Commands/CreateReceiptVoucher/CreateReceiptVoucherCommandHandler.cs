using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Commands.CreateReceiptVoucher;

public class CreateReceiptVoucherCommandHandler(
    IReceiptVouchersRepository receiptVouchersRepository,
    IMoneyAccountsRepository moneyAccountsRepository,
    ICustomersRepository customersRepository,
    IPersonsRepository personsRepository,
    IMapper mapper) : IRequestHandler<CreateReceiptVoucherCommand, long> {

    public async Task<long> Handle(CreateReceiptVoucherCommand request, CancellationToken cancellationToken) {
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
