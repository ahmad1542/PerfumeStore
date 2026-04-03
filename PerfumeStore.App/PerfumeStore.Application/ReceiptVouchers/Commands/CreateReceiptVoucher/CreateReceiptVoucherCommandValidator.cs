using FluentValidation;

namespace PerfumeStore.Application.ReceiptVouchers.Commands.CreateReceiptVoucher;

public class CreateReceiptVoucherCommandValidator : AbstractValidator<CreateReceiptVoucherCommand> {
    public CreateReceiptVoucherCommandValidator() {
        RuleFor(x => x.ReceiptForType)
            .Must(x => x == "customer" || x == "person")
            .WithMessage("Receipt type must be either customer or person.");

        RuleFor(x => x.MoneyAccountID)
            .GreaterThan(0).WithMessage("Money account is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Notes)
            .MaximumLength(250).WithMessage("Notes must be less than 250 characters.");

        When(x => x.ReceiptForType == "customer", () => {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(x => x.PersonId)
                .Must(x => !x.HasValue)
                .WithMessage("Person must be empty when receipt is for customer invoices.");

            RuleFor(x => x.SalesApplications)
                .NotEmpty().WithMessage("At least one sales invoice must be selected.");

            RuleForEach(x => x.SalesApplications).ChildRules(app => {
                app.RuleFor(x => x.SalesInvoiceId)
                    .GreaterThan(0).WithMessage("Sales invoice is required.");

                app.RuleFor(x => x.AppliedAmount)
                    .GreaterThan(0).WithMessage("Applied amount must be greater than zero.");
            });

            RuleFor(x => x)
                .Must(x => x.SalesApplications.Select(a => a.SalesInvoiceId).Distinct().Count() == x.SalesApplications.Count)
                .WithMessage("The same sales invoice cannot be selected more than once.");
        });

        When(x => x.ReceiptForType == "person", () => {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person is required.");

            RuleFor(x => x.CustomerId)
                .Must(x => !x.HasValue)
                .WithMessage("Customer must be empty when receipt is for person debts.");

            RuleFor(x => x.SalesApplications)
                .Must(x => x == null || x.Count == 0)
                .WithMessage("Sales invoice applications are not allowed for person receipt vouchers.");
        });
    }
}
