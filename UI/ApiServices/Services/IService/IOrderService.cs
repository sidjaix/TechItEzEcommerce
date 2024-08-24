using ApiServices.Models;
using ApiServices.Models.Order;

namespace ApiServices.Services.IService;

public interface IOrderService
{
    Task<List<OrderViewModel>> GetOrdersAsync();
    Task<ResponseDto> PlaceOrder(OrderViewModel newOrder);
    Task<OrderViewModel> GetOrderDetail(int orderId);
}
