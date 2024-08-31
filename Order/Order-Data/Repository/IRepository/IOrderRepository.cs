using Order_Core.Models;

namespace Order_Data.Repository.IRepository;

public interface IOrderRepository
{
    Task<OrderModel> GetOrderAsync(int orderid);
    //Task<bool> PlaceOrder(OrderModel orderDetail);
    Task<List<OrderModel>> GetOrdersAsync(string UserId);
}
