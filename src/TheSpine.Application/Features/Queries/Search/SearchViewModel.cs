using TheSpine.Core.Enums;

namespace TheSpine.Application.Features.Queries.Search
{
    public class SearchViewModel
    {
        public string Title { get; set; } = string.Empty;
        public SearchTypes Type { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
    }
}
