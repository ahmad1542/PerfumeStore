using FluentValidation;

namespace PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice {
    public class CreateSalesInvoiceCommandValidator : AbstractValidator<CreateSalesInvoiceCommand> {
        public CreateSalesInvoiceCommandValidator() {
            RuleFor(x => x.Notes).MaximumLength(250).WithMessage("Sales invoice notes must be less than 250 characters.");
            RuleFor(x => x.AmountPaid).GreaterThanOrEqualTo(0).WithMessage("Amount paid must be greater than or equal to 0.");
            RuleFor(x => x.DebtAmount).GreaterThanOrEqualTo(0).WithMessage("Debt amount must be greater than or equal to 0.");
            RuleFor(x => x.DebtNotes).MaximumLength(250).WithMessage("Debt notes must be less than 250 characters.");
        }
    }
}
