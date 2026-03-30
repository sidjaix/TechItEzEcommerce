using OrderData.Services.IServices;
using OrderApplication.Interfaces;

namespace OrderData.Persistence.Repositories;

public class OrderRepository(OrderDbContext db, IProductService productService) : IOrderRepository
{

}
