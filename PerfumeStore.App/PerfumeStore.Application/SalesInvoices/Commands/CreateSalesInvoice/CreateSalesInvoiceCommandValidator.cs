using FluentValidation;

namespace PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice {
    public class CreateSalesInvoiceCommandValidator : AbstractValidator<CreateSalesInvoiceCommand> {
        public CreateSalesInvoiceCommandValidator() {
            RuleFor(x => x.Notes)
                .MaximumLength(250)
                .WithMessage("Sales invoice notes must be less than 250 characters.");

            RuleFor(x => x.AmountPaid)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amount paid must be greater than or equal to 0.");

            RuleFor(x => x.DebtAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Debt amount must be greater than or equal to 0.");

            RuleFor(x => x.DebtNotes)
                .MaximumLength(250)
                .WithMessage("Debt notes must be less than 250 characters.");
            When(x => x.AmountPaid > 0, () => {
                RuleFor(x => x.MoneyAccountId)
                    .NotNull()
                    .WithMessage("Money account is required when amount paid is greater than zero.");
            });

            When(x => x.AmountPaid == 0, () => {
                RuleFor(x => x.MoneyAccountId)
                    .Null()
                    .WithMessage("Money account should not be selected when amount paid is zero.");

                RuleFor(x => x.HasDebt)
                    .Equal(true)
                    .WithMessage("Debt is required when amount paid is zero.");

                RuleFor(x => x.DebtAmount)
                    .NotNull()
                    .GreaterThan(0)
                    .WithMessage("Debt amount must be provided and greater than zero when amount paid is zero.");
            });

            When(x => x.HasDebt, () => {
                RuleFor(x => x.DebtAmount)
                    .NotNull()
                    .GreaterThan(0)
                    .WithMessage("Debt amount must be provided and greater than zero when HasDebt is true.");
            });

            When(x => !x.HasDebt, () => {
                RuleFor(x => x.DebtAmount)
                    .Must(x => !x.HasValue || x.Value == 0)
                    .WithMessage("Debt amount should not be provided when HasDebt is false.");
            });

            When(x => x.HasDebt && x.DebtAmount.HasValue && x.DebtAmount.Value > 0, () => {
                RuleFor(x => x.CustomerId)
                    .NotNull()
                    .WithMessage("Customer must be selected when the sales invoice has debt.");
            });

        }
    }
}
