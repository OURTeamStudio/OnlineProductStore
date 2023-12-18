using OnlineProductStore.Shared.DTO;

namespace OnlineProductStore.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderViewDTO>>> GetOrders();
        Task<ServiceResponse<OrderDetailsDTO>> GetOrderDetails(int orderId);

    }
}
