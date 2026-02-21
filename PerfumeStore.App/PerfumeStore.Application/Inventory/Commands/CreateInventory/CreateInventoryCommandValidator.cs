using FluentValidation;

namespace PerfumeStore.Application.Inventory.Commands.CreateInventory {
    public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand> {
        public CreateInventoryCommandValidator() {
            RuleFor(x => x.ProductID)
                .GreaterThan(0).WithMessage("ProductID must be greater than 0.");
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0.");
        }
    }
}
