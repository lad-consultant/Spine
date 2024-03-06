using MediatR;

using TheSpine.Application.Features.Queries.Search;
using TheSpine.Core.Enums;

namespace TheSpine.Application.Features.Queries.GetQuickLinks
{
    public class SearchQuery : IRequest<IEnumerable<SearchViewModel>>
    {
        public string Input { get; set; } = string.Empty;
        public SearchTypes Type { get; set; }
    }
}