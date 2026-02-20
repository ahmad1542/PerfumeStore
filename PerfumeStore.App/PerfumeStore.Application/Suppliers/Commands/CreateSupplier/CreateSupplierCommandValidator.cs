using FluentValidation;
using PerfumeStore.Application.Customers.Commands.CreateCustomer;

namespace PerfumeStore.Application.Suppliers.Commands.CreateSupplier {
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand> {
        public CreateSupplierCommandValidator() {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
