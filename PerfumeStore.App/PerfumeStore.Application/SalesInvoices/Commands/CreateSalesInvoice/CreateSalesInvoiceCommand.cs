using MediatR;
using PerfumeStore.Application.Products.Dtos;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice {
    public class CreateSalesInvoiceCommand : IRequest<long> {

        public DateTime Date { get; set; } = DateTime.Now;

        public string? Notes { get; set; }

        public Guid? CustomerId { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal? DebtAmount { get; set; }
        public string? DebtNotes { get; set; }
        public bool HasDebt { get; set; } = false;
        public Dictionary<int, int> Products{ get; set; } = [];
    }
}
