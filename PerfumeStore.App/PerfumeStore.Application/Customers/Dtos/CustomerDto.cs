using PerfumeStore.Application.Persons.Dtos;

namespace PerfumeStore.Application.Customers.Dtos {
    public class CustomerDto : PersonDto {
        public int TotalSalesInvoices { get; set; }
    }
}
