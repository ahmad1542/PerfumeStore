namespace PerfumeStore.Application.Inventory.Dtos {
    public class InventoryDto {
        public int ProductID { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }
    }
}
