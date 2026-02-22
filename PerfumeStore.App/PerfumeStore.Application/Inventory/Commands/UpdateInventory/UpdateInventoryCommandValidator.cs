using FluentValidation;

namespace PerfumeStore.Application.Inventory.Commands.UpdateInventory {
    public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand> {
        public UpdateInventoryCommandValidator() {
            RuleFor(x => x.ProductID)
                .GreaterThan(0).WithMessage("ProductID must be greater than 0.");
        }
    }
}
