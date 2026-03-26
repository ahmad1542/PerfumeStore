using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Commands.CreateDebt {
    public class CreateDebtCommandHandler(IDebtsRepository debtsRepository, IMapper mapper) : IRequestHandler<CreateDebtCommand, int> {
        public async Task<int> Handle(CreateDebtCommand request, CancellationToken cancellationToken) {
            if (request.SalesInvoiceId.HasValue) {
                var exists = await debtsRepository.CheckIfSalesInvoiceExist(request.SalesInvoiceId.Value);
                if (!exists) {
                    throw new NotFoundException(nameof(SalesInvoice), request.SalesInvoiceId.Value.ToString());
                }
            }

            if (request.PurchaseInvoiceId.HasValue) {
                var exists = await debtsRepository.CheckIfPurchaseInvoiceExist(request.PurchaseInvoiceId.Value);
                if (!exists) {
                    throw new NotFoundException(nameof(PurchaseInvoice), request.PurchaseInvoiceId.Value.ToString());
                }
            }

            if (request.PersonId.HasValue) {
                var exists = await debtsRepository.CheckIfPersonExist(request.PersonId.Value);
                if (!exists) {
                    throw new NotFoundException(nameof(Person), request.PersonId.Value.ToString());
                }
            }

            var debt = mapper.Map<Debt>(request);
            var id = await debtsRepository.AddAsync(debt);
            return id;
        }
    }
}