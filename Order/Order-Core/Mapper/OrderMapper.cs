using Order_Core.Entities;
using Order_Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace Order_Core.Mapper;

public static class OrderMapper
{
    public static Order MapToEntity(this OrderModel order)
    {
        var orderEntity = new Order()
        {
            OrderId = order.OrderId,
            OrderStatusId = order.OrderStatusId,
            OrderDate = order.OrderDate,
            OrderTotal = order.OrderTotal,
            PaymentMethodId = order.PaymentMethodId,
            AddressId = order.AddressId,
            UserId = order.UserId,
            OrderDetails = order.OrderDetails?.Select(x => new OrderDetail
            {
                OrderDetialId = x.OrderDetialId,
                OrderId = x.OrderId,
                Price = x.Price,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };
        return orderEntity;
    }

    public static OrderModel MapToDto(this Order order)
    {
        var orderModel = new OrderModel()
        {
            OrderId = order.OrderId,
            OrderStatusId = order.OrderStatusId,
            OrderDate = order.OrderDate,
            OrderTotal = order.OrderTotal,
            PaymentMethodId = order.PaymentMethodId,
            AddressId = order.AddressId,
            UserId = order.UserId,
            Status = order.Status,
            OrderDetails = order.OrderDetails?.Select(x => new OrderDetailModel
            {
                OrderDetialId = x.OrderDetialId,
                OrderId = x.OrderId,
                Price = x.Price,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList() ?? []
        };
        return orderModel;
    }
}
