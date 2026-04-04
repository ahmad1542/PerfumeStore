using FluentValidation;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommandValidator : AbstractValidator<UpdateDebtCommand> {
        public UpdateDebtCommandValidator() {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount is required and must be greater than 0.");

            RuleFor(x => x.MoneyAccountId)
                .GreaterThan(0)
                .WithMessage("Debt must have money account id.");

            RuleFor(x => x.PersonId)
                .NotNull()
                .WithMessage("Person is required.");

            RuleFor(x => x.Notes)
                .MaximumLength(250)
                .WithMessage("Notes must not exceed 250 characters.");
        }
    }
}