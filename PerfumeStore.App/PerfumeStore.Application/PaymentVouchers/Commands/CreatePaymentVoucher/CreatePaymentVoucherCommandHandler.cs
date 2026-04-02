using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PaymentVouchers.Commands.CreatePaymentVoucher;

public class CreatePaymentVoucherCommandHandler(
    IPaymentVouchersRepository paymentVouchersRepository,
    IMoneyAccountsRepository moneyAccountsRepository,
    ISuppliersRepository suppliersRepository,
    IMapper mapper) : IRequestHandler<CreatePaymentVoucherCommand, long> {

    public async Task<long> Handle(CreatePaymentVoucherCommand request, CancellationToken cancellationToken) {
        var supplier = await suppliersRepository.GetByIdAsync(request.SupplierId);
        if (supplier == null) {
            throw new Exception("Selected supplier was not found.");
        }

        var account = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountID);
        if (account == null) {
            throw new Exception("Selected money account was not found.");
        }

        if (account.CurrentBalance < request.Amount) {
            throw new Exception("The selected money account does not have enough balance.");
        }

        var totalApplied = request.Applications.Sum(x => x.AppliedAmount);
        if (Math.Abs(totalApplied - request.Amount) >= 0.01m) {
            throw new Exception("Voucher amount must equal the total applied amount.");
        }

        var paymentVoucher = mapper.Map<PaymentVoucher>(request);
        var applications = mapper.Map<List<PaymentVoucherPurchaseInvoice>>(request.Applications);

        var id = await paymentVouchersRepository.AddAsync(paymentVoucher, applications);

        account.CurrentBalance -= request.Amount;
        await moneyAccountsRepository.SaveChangesAsync();

        return id;
    }
}
