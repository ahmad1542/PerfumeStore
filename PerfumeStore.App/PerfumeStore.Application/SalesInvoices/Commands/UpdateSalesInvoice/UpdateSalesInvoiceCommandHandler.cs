using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Commands.UpdateSalesInvoice {
    public class UpdateSalesInvoiceCommandHandler(ISalesInvoicesRepository salesInvoicesRepository, IMapper mapper) : IRequestHandler<UpdateSalesInvoiceCommand> {
        public async Task Handle(UpdateSalesInvoiceCommand request, CancellationToken cancellationToken) {
            var salesInvoice = await salesInvoicesRepository.GetByIdAsync(request.ID);
            if (salesInvoice == null)
                throw new NotFoundException(nameof(SalesInvoice), request.ID.ToString());
            if (request.HasDebt) {
                if (salesInvoice.Debt != null) {
                    salesInvoice.Debt.Amount = request.DebtAmount ?? 0;
                    salesInvoice.Debt.Notes = request.DebtNotes;
                } else {
                    salesInvoice.Debt = new Debt {
                        Amount = request.DebtAmount ?? 0,
                        Notes = request.DebtNotes
                    };
                }
            } else {
                if (salesInvoice.Debt != null) {
                    salesInvoice.Debt.Amount = 0;
                    salesInvoice.Debt.Notes = null;
                }
            }
            mapper.Map(request, salesInvoice);
            await salesInvoicesRepository.SaveChangesAsync();
        }
    }
}
