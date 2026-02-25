using FluentValidation;

namespace PerfumeStore.Application.MoneyAccounts.Commands.UpdateMoneyAccount {
    public class UpdateMoneyAccountCommandValidator : AbstractValidator<UpdateMoneyAccountCommand> {
        public UpdateMoneyAccountCommandValidator() {
            RuleFor(x => x.AccountName).NotEmpty().WithMessage("Account name is required.").MaximumLength(50).WithMessage("Account name cannot exceed 100 characters.");
            RuleFor(x => x.CurrentBalance).GreaterThanOrEqualTo(0).WithMessage("Current balance must be greater than or equal to zero.");
            RuleFor(x => x.Notes).MaximumLength(250).WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}
