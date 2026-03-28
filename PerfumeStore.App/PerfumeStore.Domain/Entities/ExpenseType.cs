using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities {
    public class ExpenseType {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
