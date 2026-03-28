using FluentValidation;

namespace PerfumeStore.Application.ExpenseTypes.Commands.CreateExpenseType {
    public class CreateExpenseTypeCommandValidator : AbstractValidator<CreateExpenseTypeCommand> {
        public CreateExpenseTypeCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Expense type name is required.")
                .MaximumLength(150).WithMessage("Expense type name cannot exceed 150 characters.");
        }
    }
}
