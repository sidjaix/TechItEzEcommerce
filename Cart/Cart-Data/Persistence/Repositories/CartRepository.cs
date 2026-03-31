using CartApplication.Interfaces;
using CartCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartData.Persistence.Repositories;

public class CartRepository(CartDbContext _context) : ICartRepository
{
    public async Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
    }

    public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var cart = await GetByCustomerIdAsync(customerId, cancellationToken);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
