using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task PlaceOrder();
        Task<List<OrderViewDTO>> GetOrders();
        Task<OrderDetailsDTO> GetOrderDetails(int orderId);
    }
}
