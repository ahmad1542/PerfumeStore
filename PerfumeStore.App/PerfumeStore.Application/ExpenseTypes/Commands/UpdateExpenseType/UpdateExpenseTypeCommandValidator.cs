using FluentValidation;

namespace PerfumeStore.Application.ExpenseTypes.Commands.UpdateExpenseType {
    public class UpdateExpenseTypeCommandValidator : AbstractValidator<UpdateExpenseTypeCommand> {
        public UpdateExpenseTypeCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Expense type name is required.")
                .MaximumLength(150).WithMessage("Expense type name cannot exceed 150 characters.");
        }
    }
}
