using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities {
    public class Person : BaseEntity {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public string Phone { get; private set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public ICollection<Debt> Debts { get; private set; } = new List<Debt>();

        public void ChangePhone(string newPhone) {
            if (Phone == newPhone) return;
            Phone = newPhone;
        }
    }
}
