using FluentValidation;
using PerfumeStore.Application.Debts.Commands.CreateDebt;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommandValidator : AbstractValidator<UpdateDebtCommand> {
        public UpdateDebtCommandValidator() {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount is required and must be greater than 0.");
            RuleFor(x => x.Notes)
                .MinimumLength(250)
                .WithMessage("Notes must be at least 250 characters long.");
            RuleFor(x => x)
                .Must(HasExactlyOneLink)
                .WithMessage("Debt must be linked to exactly one of: Sales Invoice, Purchase Invoice, or Person.");

            When(x => x.SalesInvoiceId.HasValue, () => {
                RuleFor(x => x.SalesInvoiceId!.Value).GreaterThan(0);
            });

            When(x => x.PurchaseInvoiceId.HasValue, () => {
                RuleFor(x => x.PurchaseInvoiceId!.Value).GreaterThan(0);
            });

            // Notes required only when linking to person
            When(x => x.PersonId.HasValue, () => {
                RuleFor(x => x.Notes)
                    .NotEmpty()
                    .WithMessage("Notes are required when debt is linked to a person.");
            });
        }

        private static bool HasExactlyOneLink(UpdateDebtCommand x) {
            var count =
                (x.SalesInvoiceId.HasValue ? 1 : 0) +
                (x.PurchaseInvoiceId.HasValue ? 1 : 0) +
                (x.PersonId.HasValue ? 1 : 0);

            return count == 1;
        }
    }
}
