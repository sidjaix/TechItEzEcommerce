using App_Contracts.Order;
using MassTransit;

using Order_Core;
using Order_Core.Entities;
using Order_Core.Enums;

namespace Order_Service.EventHandlers;

public class OrderCreateHandler(ILogger<OrderCreateHandler> logger, OrderDbContext db) : IConsumer<OrderCreateEvent>
{
    public async Task Consume(ConsumeContext<OrderCreateEvent> orderContext)
    {
        logger.LogInformation("Processiong the order...");
        var newOrder = orderContext.Message;
        //logger.LogInformation($"Order Detail: \n Order Date: {newOrder.OrderDate.Date}, \n Order Total: {newOrder.OrderTotal}");

        var order = new Order()
        {
            UserId = newOrder.UserId,
            PaymentMethodId = newOrder.PaymentMethodId,
            OrderDate = newOrder.OrderDate,
            OrderTotal = newOrder.OrderTotal,
            AddressId = newOrder.AddressId,
            OrderStatusId = (int)OrderStatusEnum.Processing,
            OrderDetails = newOrder.OrderItems?.Select(x => new OrderDetail
            {
                Price = x.Price,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };

        db.Orders.Add(order);
        await db.SaveChangesAsync();
    }
}
