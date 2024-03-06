using Mapster;

using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Queries.Search;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetQuickLinks
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<SearchViewModel>>
    {
        private readonly IItemDetailedInfoAsyncRepository _repository;
        private readonly ILogger<SearchQueryHandler> logger;

        public SearchQueryHandler(
            IItemDetailedInfoAsyncRepository repository,
            ILogger<SearchQueryHandler> logger)
        {
            _repository = repository;
            this.logger = logger;
        }

        public async Task<IEnumerable<SearchViewModel>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the search query.");

            var items = await _repository.Search(request.Input);

            logger.LogInformation("Search query is finished.");

            return items.Adapt<IEnumerable<SearchViewModel>>();
        }
    }
}