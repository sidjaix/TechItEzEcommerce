using OrderCore.Entities;

namespace OrderApplication.Interfaces;

public interface IOrderRepository
{
	Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
	Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
	Task AddAsync(Order order, CancellationToken cancellationToken);
	Task UpdateAsync(Order order, CancellationToken cancellationToken);
}
