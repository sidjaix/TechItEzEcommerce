using OrderApplication.Interfaces;
using OrderCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderData.Persistence.Repositories;

public class OrderRepository(OrderDbContext db) : IOrderRepository
{
	public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
	{
		return await db.Orders
			.Include(o => o.Items)
			.Where(o => o.CustomerId == customerId)
			.OrderByDescending(o => o.OrderDate)
			.AsNoTracking()
			.ToListAsync(ct);
	}

	public async Task<Order> GetByIdAsync(Guid orderId, CancellationToken ct)
	{
		return await db.Orders
			.Include(o => o.Items)
			.FirstOrDefaultAsync(o => o.Id == orderId, ct);
	}

	public async Task AddAsync(Order order, CancellationToken ct)
	{
		await db.Orders.AddAsync(order, ct);
		await db.SaveChangesAsync(ct);
	}

	public async Task UpdateAsync(Order order, CancellationToken ct)
	{
		db.Orders.Update(order);
		await db.SaveChangesAsync(ct);
	}
}
