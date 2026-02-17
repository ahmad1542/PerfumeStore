using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities {
    public class Person : BaseEntity {
        [Key]
        public string Phone { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public ICollection<Debt> Debts { get; private set; } = new List<Debt>();

    }
}
