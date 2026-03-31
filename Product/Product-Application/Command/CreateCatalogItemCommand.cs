using MediatR;
using ProductApplication.Interfaces;
using ProductCore.Entities;
using System;

namespace ProductApplication.Command;

public record CreateCatalogItemCommand(string BaseName, string Slug, Guid CategoryId, Guid BrandId) : IRequest<Guid>;

public class CreateCatalogItemCommandHandler : IRequestHandler<CreateCatalogItemCommand, Guid>
{
    private readonly ICatalogRepository _repository;
    public CreateCatalogItemCommandHandler(ICatalogRepository repository) { _repository = repository; }

    public async Task<Guid> Handle(CreateCatalogItemCommand request, CancellationToken cancellationToken)
    {
        var item = new CatalogItem(request.BaseName, request.Slug, request.CategoryId, request.BrandId);

        await _repository.AddAsync(item, cancellationToken);
        return item.Id;
    }
}