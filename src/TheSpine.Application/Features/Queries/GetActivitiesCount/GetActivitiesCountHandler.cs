using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetActivitiesCount
{
    public class GetActivitiesCountHandler : IRequestHandler<GetActivitiesCountQuery, int>
    {
        private readonly IActivityAsyncRepository repository;
        private readonly ILogger<GetActivitiesCountHandler> logger;

        public GetActivitiesCountHandler(
            IActivityAsyncRepository repository,
            ILogger<GetActivitiesCountHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<int> Handle(GetActivitiesCountQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get activities count query.");

            var count = await repository.GetActivityRowsCount();

            logger.LogInformation("Get activities count query finished.");

            return count;
        }
    }
}
