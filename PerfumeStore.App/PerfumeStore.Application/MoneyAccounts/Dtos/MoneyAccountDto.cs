using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Application.MoneyAccounts.Dtos {
    public class MoneyAccountDto {
        public int ID { get; set; }

        public string AccountName { get; set; } = null!;

        public decimal CurrentBalance { get; set; }

        public string? Notes { get; set; }
    }
}
