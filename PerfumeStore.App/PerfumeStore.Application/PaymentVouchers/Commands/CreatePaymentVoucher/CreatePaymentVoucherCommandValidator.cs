using FluentValidation;

namespace PerfumeStore.Application.PaymentVouchers.Commands.CreatePaymentVoucher;

public class CreatePaymentVoucherCommandValidator : AbstractValidator<CreatePaymentVoucherCommand> {
    public CreatePaymentVoucherCommandValidator() {
        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Supplier is required.");

        RuleFor(x => x.MoneyAccountID)
            .GreaterThan(0).WithMessage("Money account is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Notes)
            .MaximumLength(250).WithMessage("Notes must be less than 250 characters.");

        RuleFor(x => x.Applications)
            .NotEmpty().WithMessage("At least one purchase invoice must be selected.");

        RuleForEach(x => x.Applications).ChildRules(app => {
            app.RuleFor(x => x.PurchaseInvoiceId)
                .GreaterThan(0).WithMessage("Purchase invoice is required.");

            app.RuleFor(x => x.AppliedAmount)
                .GreaterThan(0).WithMessage("Applied amount must be greater than zero.");
        });

        RuleFor(x => x)
            .Must(x => x.Applications.Select(a => a.PurchaseInvoiceId).Distinct().Count() == x.Applications.Count)
            .WithMessage("The same purchase invoice cannot be selected more than once.");

        RuleFor(x => x)
            .Must(x => Math.Abs(x.Applications.Sum(a => a.AppliedAmount) - x.Amount) < 0.01m)
            .WithMessage("Voucher amount must equal the total applied amount.");
    }
}
