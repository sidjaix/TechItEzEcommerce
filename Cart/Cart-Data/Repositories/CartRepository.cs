using Cart_Core;
using Cart_Data.Repositories.IRepositories;

namespace Cart_Data.Repositories;

public class CartRepository(CartDbContext db) : ICartRepository
{

}
