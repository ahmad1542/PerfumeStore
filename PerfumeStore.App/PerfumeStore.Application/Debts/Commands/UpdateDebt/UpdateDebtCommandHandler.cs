using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommandHandler(IDebtsRepository debtsRepository, IMapper mapper) : IRequestHandler<UpdateDebtCommand> {
        public async Task Handle(UpdateDebtCommand request, CancellationToken cancellationToken) {
            var debt = await debtsRepository.GetByIdAsync(request.Id);
            if (debt is null) {
                throw new NotFoundException(nameof(Debt), request.Id.ToString());
            }

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

            mapper.Map(request, debt);
            await debtsRepository.SaveChangesAsync();
        }
    }
}