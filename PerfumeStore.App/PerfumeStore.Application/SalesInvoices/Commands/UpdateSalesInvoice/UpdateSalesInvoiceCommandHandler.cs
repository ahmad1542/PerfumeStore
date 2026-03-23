using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Commands.UpdateSalesInvoice {
    public class UpdateSalesInvoiceCommandHandler(ISalesInvoicesRepository salesInvoicesRepository, IDebtsRepository debtsRepository, IMapper mapper) : IRequestHandler<UpdateSalesInvoiceCommand> {
        public async Task Handle(UpdateSalesInvoiceCommand request, CancellationToken cancellationToken) {

            var salesInvoice = await salesInvoicesRepository.GetByIdAsync(request.ID);

            if (salesInvoice == null)
                throw new NotFoundException(nameof(SalesInvoice), request.ID.ToString());

            salesInvoice.Date = request.Date;
            salesInvoice.Notes = request.Notes;
            salesInvoice.CustomerId = request.CustomerId;
            salesInvoice.AmountPaid = request.AmountPaid;

            if (request.HasDebt && request.DebtAmount.HasValue && request.DebtAmount.Value > 0) {
                if (salesInvoice.Debt != null) {
                    salesInvoice.Debt.Amount = request.DebtAmount.Value;
                    salesInvoice.Debt.Notes = request.DebtNotes;
                    salesInvoice.Debt.PersonId = request.CustomerId;
                    salesInvoice.Debt.SalesInvoiceId = salesInvoice.ID;
                } else {
                    salesInvoice.Debt = new Debt {
                        Amount = request.DebtAmount.Value,
                        Notes = request.DebtNotes,
                        PersonId = request.CustomerId,
                        SalesInvoiceId = salesInvoice.ID
                    };
                }
            } else {
                if (salesInvoice.Debt != null) {
                    await debtsRepository.SoftDeleteAsync(salesInvoice.Debt);
                    salesInvoice.Debt = null;
                }
            }

            await salesInvoicesRepository.SaveChangesAsync();

            await salesInvoicesRepository.UpdateProductsAsync(salesInvoice.ID, request.Products);
        }
    }
}
