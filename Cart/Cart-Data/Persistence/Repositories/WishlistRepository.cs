using CartApplication.Interfaces;
using CartData.Services.IServices;

namespace CartData.Persistence.Repositories;

public class WishlistRepository(CartDbContext db, IProductService productService) : IWishlistRepository
{

}
