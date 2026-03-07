namespace PerfumeStore.Application.SalesInvoiceItems.Dtos {
    public class SalesInvoicesItemDto {
        public long ID { get; set; }

        public int Quantity { get; set; }

        public long SalesInvoiceID { get; set; }

        public int ProductID { get; set; }
    }
}
