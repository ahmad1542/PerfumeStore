using PerfumeStore.Application.Persons.Dtos;

namespace PerfumeStore.Application.Suppliers.Dtos {
    public class SupplierDto : PersonDto {
        public int TotalPurchaseInvoices { get; set; }
    }
}
