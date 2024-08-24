using ApiServices.Models;
using ApiServices.Models.Order;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;
using Newtonsoft.Json;

namespace ApiServices.Services;

public class OrderService(IBaseService baseService) : IOrderService
{
    public async Task<List<OrderViewModel>> GetOrdersAsync()
    {
        var orders = new List<OrderViewModel>();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.OrderApiBaseAddress}/api/order/GetOrders",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
        }

        return orders;
    }

    public async Task<ResponseDto> PlaceOrder(OrderViewModel newOrder)
    {
        ResponseDto orderResponse = new();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/order/PlaceOrder",
            ApiMethod = ApiMethod.POST,
            Data = newOrder,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            orderResponse = JsonConvert.DeserializeObject<ResponseDto>(Convert.ToString(response));
        }
        return orderResponse;
    }

    public async Task<OrderViewModel> GetOrderDetail(int orderId)
    {
        OrderViewModel orderDetail = new();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/order/GetOrder/{orderId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            orderDetail = JsonConvert.DeserializeObject<OrderViewModel>(Convert.ToString(response.Result));
        }
        return orderDetail;
    }
}
