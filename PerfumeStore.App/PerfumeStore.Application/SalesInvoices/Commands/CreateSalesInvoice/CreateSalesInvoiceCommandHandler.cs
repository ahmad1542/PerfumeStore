using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice {
    public class CreateSalesInvoiceCommandHandler(ISalesInvoicesRepository salesInvoicesRepository, IMapper mapper) : IRequestHandler<CreateSalesInvoiceCommand, long> {
        public async Task<long> Handle(CreateSalesInvoiceCommand request, CancellationToken cancellationToken) {
            var salesInvoice = mapper.Map<SalesInvoice>(request);
            await salesInvoicesRepository.AddAsync(salesInvoice);
            return salesInvoice.ID;
        }
    }
}
