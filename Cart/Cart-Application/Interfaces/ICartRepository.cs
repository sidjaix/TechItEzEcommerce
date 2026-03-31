using CartCore.Entities;

namespace CartApplication.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task AddAsync(Cart cart, CancellationToken cancellationToken);
    Task UpdateAsync(Cart cart, CancellationToken cancellationToken);
    Task DeleteAsync(Guid customerId, CancellationToken cancellationToken);
}
