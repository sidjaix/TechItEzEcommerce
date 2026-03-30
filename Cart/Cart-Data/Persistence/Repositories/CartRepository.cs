
using CartData.Services.IServices;
using CartApplication.Interfaces;

namespace CartData.Persistence.Repositories;

public class CartRepository(CartDbContext db, IProductService productService) : ICartRepository
{
}
