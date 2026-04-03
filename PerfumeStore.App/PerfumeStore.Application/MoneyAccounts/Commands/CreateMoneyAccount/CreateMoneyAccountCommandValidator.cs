using FluentValidation;

namespace PerfumeStore.Application.MoneyAccounts.Commands.CreateMoneyAccount {
    public class CreateMoneyAccountCommandValidator : AbstractValidator<CreateMoneyAccountCommand> {
        public CreateMoneyAccountCommandValidator() {
            RuleFor(x => x.AccountName)
                .NotEmpty()
                .WithMessage("Account name is required.")
                .MaximumLength(50)
                .WithMessage("Account name cannot exceed 100 characters.");
            RuleFor(x => x.Notes)
                .MaximumLength(250)
                .WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}
