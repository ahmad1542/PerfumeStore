using FluentValidation;

namespace PerfumeStore.Application.Suppliers.Commands.UpdateSupplier {
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand> {
        public UpdateSupplierCommandValidator() {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
