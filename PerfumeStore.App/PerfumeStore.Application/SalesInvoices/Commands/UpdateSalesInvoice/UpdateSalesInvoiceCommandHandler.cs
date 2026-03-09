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
            mapper.Map(request, salesInvoice);
            await salesInvoicesRepository.SaveChangesAsync();
        }
    }
}
