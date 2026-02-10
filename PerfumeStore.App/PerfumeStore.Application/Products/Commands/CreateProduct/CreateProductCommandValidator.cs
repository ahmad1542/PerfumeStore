using FluentValidation;

namespace PerfumeStore.Application.Products.Commands.CreateProduct {
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
        public CreateProductCommandValidator() {
            RuleFor(dto => dto.Name).Length(3, 150);
            RuleFor(dto => dto.Capacity).GreaterThan(0);
            RuleFor(dto => dto.SalePrice).GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.MinStock).GreaterThanOrEqualTo(0);
        }
    }
}
