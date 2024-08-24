using MassTransit;
using Order_Core.Models;

namespace OrderService;

public class OrderConsumer(ILogger<OrderConsumer> logger) : IConsumer<OrderModel>
{
    public async Task Consume(ConsumeContext<OrderModel> context)
    {
        logger.LogInformation("Processiong the order...");
        var newOrder = context.Message;
        logger.LogInformation($"Order Detail: \n Order Date: {newOrder.OrderDate.Date}, \n Order Total: {newOrder.OrderTotal}");
    }
}
