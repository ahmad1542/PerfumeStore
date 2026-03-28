using FluentValidation;

namespace PerfumeStore.Application.Expenses.Commands.CreateExpense {
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand> {
        public CreateExpenseCommandValidator() {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Notes)
                .MaximumLength(250).WithMessage("Notes cannot exceed 250 characters.");

            RuleFor(x => x.ExpenseTypeId)
                .GreaterThan(0).WithMessage("Expense type is required.");

            RuleFor(x => x.MoneyAccountID)
                .GreaterThan(0).WithMessage("Money account is required.");
        }
    }
}
