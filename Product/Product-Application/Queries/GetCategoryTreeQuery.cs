using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;
using ProductCore.Entities;
using System;

namespace ProductApplication.Queries;

public record GetCategoryTreeQuery : IRequest<IEnumerable<CategoryNodeDto>>;

public class GetCategoryTreeQueryHandler : IRequestHandler<GetCategoryTreeQuery, IEnumerable<CategoryNodeDto>>
{
    private readonly ITaxonomyRepository _repository;
    public GetCategoryTreeQueryHandler(ITaxonomyRepository repository) => _repository = repository;

    public async Task<IEnumerable<CategoryNodeDto>> Handle(GetCategoryTreeQuery request, CancellationToken ct)
    {
        var tree = await _repository.GetCategoryTreeAsync(ct);
        return MapNodes(tree);
    }

    private List<CategoryNodeDto> MapNodes(IEnumerable<Category> categories)
    {
        return categories.Select(c => new CategoryNodeDto
        {
            Id = c.Id,
            Name = c.Name,
            SubCategories = MapNodes(c.SubCategories)
        }).ToList();
    }
}