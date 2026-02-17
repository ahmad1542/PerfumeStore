using FluentValidation;

namespace PerfumeStore.Application.Persons.Commands.UpdatePerson {
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand> {
        public UpdatePersonCommandValidator() {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.").MinimumLength(2).WithMessage("Name must be at least 2 characters.")
                .MaximumLength(150).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
