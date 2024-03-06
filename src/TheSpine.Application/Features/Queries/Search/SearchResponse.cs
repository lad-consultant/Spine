using TheSpine.Application.Features.Commands;

namespace TheSpine.Application.Features.Queries.Search
{
    public class SearchResponse : Response
    {
        IEnumerable<SearchViewModel> Items { get; set; }
    }
}
