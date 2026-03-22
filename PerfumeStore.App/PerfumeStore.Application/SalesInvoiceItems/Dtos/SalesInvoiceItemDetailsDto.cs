namespace PerfumeStore.Application.SalesInvoiceItems.Dtos {
    public class SalesInvoiceItemDetailsDto {
        public long ID { get; set; }

        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
