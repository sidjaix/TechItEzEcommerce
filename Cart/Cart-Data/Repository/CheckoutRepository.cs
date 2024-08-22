using Cart_Core;
using Cart_Data.Repository.IRepository;

namespace Cart_Data.Repository;

public class CheckoutRepository(CartDbContext db) : ICheckoutRepository
{

}
