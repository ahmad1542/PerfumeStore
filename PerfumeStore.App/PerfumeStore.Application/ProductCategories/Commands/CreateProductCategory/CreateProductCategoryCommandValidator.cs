using FluentValidation;

namespace PerfumeStore.Application.ProductCategories.Commands.CreateProductCategory {
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand> {
        public CreateProductCategoryCommandValidator() {
            RuleFor(c => c.Name).Length(3, 100);
        }
    }
}
