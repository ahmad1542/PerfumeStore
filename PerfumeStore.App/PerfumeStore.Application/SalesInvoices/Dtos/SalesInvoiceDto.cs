using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.SalesInvoices.Dtos {
    public class SalesInvoiceDto {
        public long ID { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string? Notes { get; set; }

        public string? CustomerName { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal? DebtAmount { get; set; }
    }
}
