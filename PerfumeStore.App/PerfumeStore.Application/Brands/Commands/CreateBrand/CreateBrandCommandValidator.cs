using FluentValidation;

namespace PerfumeStore.Application.Brands.Commands.CreateBrand {
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand> {
        public CreateBrandCommandValidator() {
            RuleFor(dto => dto.Name).Length(3, 150);
        }
    }
}
