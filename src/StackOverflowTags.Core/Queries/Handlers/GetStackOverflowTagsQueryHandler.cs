using Mediator;
using StackOverflowTags.Core.DTOs;

namespace StackOverflowTags.Core.Queries.Handlers;

internal sealed class GetStackOverflowTagsQueryHandler : IQueryHandler<GetStackOverflowTagsQuery, IReadOnlyList<TagDto>>
{
    private readonly HttpClient _http;

    public GetStackOverflowTagsQueryHandler(IHttpClientFactory httpFactory)
        => _http = httpFactory.CreateClient("stackExchangeApi");

    public async ValueTask<IReadOnlyList<TagDto>> Handle(
        GetStackOverflowTagsQuery qry,
        CancellationToken ct
    )
    {
        var res = await _http.Get(
            $"tags?order=desc&sort=popular&site=stackoverflow&page_size=100",
            GetStackOverflowTagsResponseDtoJsonContext.Default.GetStackOverflowTagsResponseDto,
            ct
        );
        var totalUses = res.Items.Sum(tag => tag.Count);
        var tags = new List<TagDto>();

        return res.Items
            .Select(q => new TagDto(
                q.Name,
                q.Count,
                q.Count / (double)totalUses * 100
            ))
            .ToList();
    }
}