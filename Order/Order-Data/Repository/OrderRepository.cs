using Microsoft.EntityFrameworkCore;
using Order_Core;
using Order_Core.Entities;
using Order_Core.Mapper;
using Order_Core.Models;
using Order_Data.Repository.IRepository;
using Order_Data.Services.IServices;

namespace Order_Data.Repository;

public class OrderRepository(OrderDbContext db, IProductService productService) : IOrderRepository
{
    public async Task<List<OrderModel>> GetOrdersAsync(string UserId)
    {
        var products = await productService.GetProductsAsync();

        var orders = await (from o in db.Orders.Include(od => od.OrderDetails)
                            join s in db.OrderStatuses on o.OrderStatusId equals s.OrderStatusId
                            where o.UserId == UserId
                            select new Order
                            {
                                OrderId = o.OrderId,
                                // OrderStatusId = o.OrderStatusId,
                                OrderDate = o.OrderDate,
                                //OrderTotal = o.OrderTotal,
                                //PaymentMethodId = o.PaymentMethodId,
                                //AddressId = o.AddressId,
                                //UserId = o.UserId,
                                Status = s.Status,
                                OrderDetails = o.OrderDetails
                            }).Select(x => x.MapToDto()).ToListAsync();

        foreach (var order in orders)
        {
            order.OrderDetails = (from od in order.OrderDetails
                                  join p in products on od.ProductId equals p.ProductId
                                  select new OrderDetailModel
                                  {
                                      OrderDetialId = od.OrderDetialId,
                                      //OrderId = od.OrderId,
                                      //Price = od.Price,
                                      ProductId = od.ProductId,
                                      //Quantity = od.Quantity,
                                      ProductName = p.ProductName,
                                      ProductImageUrl = p.ImageUrl
                                  }).ToList();
        }
        return orders;
    }

    public async Task<OrderModel> GetOrderAsync(int orderId)
    {
        var products = await productService.GetProductsAsync();

        var order = await (from o in db.Orders.Include(od => od.OrderDetails)
                           join s in db.OrderStatuses on o.OrderStatusId equals s.OrderStatusId
                           where o.OrderId == orderId
                           select new Order
                           {
                               OrderId = o.OrderId,
                               OrderStatusId = o.OrderStatusId,
                               OrderDate = o.OrderDate,
                               OrderTotal = o.OrderTotal,
                               PaymentMethodId = o.PaymentMethodId,
                               AddressId = o.AddressId,
                               UserId = o.UserId,
                               Status = s.Status,
                               OrderDetails = o.OrderDetails
                           }).Select(x => x.MapToDto()).FirstOrDefaultAsync();

        if (order is not null)
        {
            order.OrderDetails = (from od in order.OrderDetails
                                  join p in products on od.ProductId equals p.ProductId
                                  select new OrderDetailModel
                                  {
                                      OrderDetialId = od.OrderDetialId,
                                      OrderId = od.OrderId,
                                      Price = od.Price,
                                      ProductId = od.ProductId,
                                      Quantity = od.Quantity,
                                      ProductName = p.ProductName,
                                      ProductImageUrl = p.ImageUrl
                                  }).ToList();
        }
        return order;
    }

    public async Task<bool> PlaceOrder(OrderModel orderDetail)
    {
        var order = orderDetail.MapToEntity();
        db.Orders.Add(order);
        var isCreated = await db.SaveChangesAsync() > 0;
        return isCreated;
    }
}
