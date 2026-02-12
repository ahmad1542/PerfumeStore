using FluentValidation;

namespace PerfumeStore.Application.ProductCategories.Commands.UpdateProductCategory {
    public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand> {
        public UpdateProductCategoryCommandValidator() {
            RuleFor(c => c.Name).Length(3, 100);
        }
    }
}
