using MediatR;
using ProductApplication.Interfaces;
using ProductCore.Entities;

namespace ProductApplication.Command;

public record AddProductReviewCommand(Guid CatalogItemId, Guid CustomerId, string ReviewerAlias, int Rating, string Text) : IRequest<bool>;

public class AddProductReviewCommandHandler : IRequestHandler<AddProductReviewCommand, bool>
{
    private readonly ICatalogRepository _repository;
    public AddProductReviewCommandHandler(ICatalogRepository repo) => _repository = repo;

    public async Task<bool> Handle(AddProductReviewCommand request, CancellationToken ct)
    {
        var item = await _repository.GetByIdAsync(request.CatalogItemId, ct);
        if (item == null) return false;

        // Uses the patched List<ProductReview> internal access
        // Ideally, CatalogItem would have an .AddReview() method similarly to .AddVariant()
        item.Reviews.ToList().Add(new ProductReview(item.Id, request.CustomerId, request.Rating, request.ReviewerAlias, request.Text));

        await _repository.UpdateAsync(item, ct);
        return true;
    }
}
