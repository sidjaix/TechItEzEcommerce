using CartApplication.Interfaces;
using MediatR;

namespace CartApplication.Commands;

public record ClearCartCommand(Guid CustomerId) : IRequest<bool>;

public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
{
    private readonly ICartRepository _repository;
    public ClearCartCommandHandler(ICartRepository repository) => _repository = repository;

    public async Task<bool> Handle(ClearCartCommand request, CancellationToken ct)
    {
        await _repository.DeleteAsync(request.CustomerId, ct);
        return true;
    }
}