using AppContracts.Order;
using MassTransit;
using OrderData.Persistence;

namespace OrderService.EventHandlers;

public class OrderCreateHandler(ILogger<OrderCreateHandler> logger, OrderDbContext db) : IConsumer<OrderCreateEvent>
{
    public async Task Consume(ConsumeContext<OrderCreateEvent> orderContext)
    {
        logger.LogInformation("Processiong the order...");
        var newOrder = orderContext.Message;
        //logger.LogInformation($"Order Detail: \n Order Date: {newOrder.OrderDate.Date}, \n Order Total: {newOrder.OrderTotal}");
        // var order = new Order();
        // db.Orders.Add(order);
        await db.SaveChangesAsync();
    }
}
