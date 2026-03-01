using FluentValidation;

namespace PerfumeStore.Application.MoneyTransactions.Commands.UpdateMoneyTransaction {
    public class UpdateMoneyTransactionCommandValidator : AbstractValidator<UpdateMoneyTransactionCommand> {
        public UpdateMoneyTransactionCommandValidator() {
            RuleFor(x => x.TransferAmount)
                .GreaterThan(0).WithMessage("Transfer amount must be greater than 0.");
            RuleFor(x => x.Notes)
                .MaximumLength(250).WithMessage("Notes must be at most 250 character.");
            RuleFor(x => x.FromMoneyAccountID)
                .NotEqual(x => x.ToMoneyAccountID).WithMessage("From and To Money Account must be different.");
            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Now.AddDays(1))
                .When(x => x.Date.HasValue);
        }
    }
}
