namespace OnlineProductStore.Shared.DTO
{
    public class OrderDetailsDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDetailsDTO> Products { get; set; }
    }
}
