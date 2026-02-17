namespace PerfumeStore.Application.Persons.Dtos {
    public class PersonDto {
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal TotalDebt { get; set; }
    }
}
