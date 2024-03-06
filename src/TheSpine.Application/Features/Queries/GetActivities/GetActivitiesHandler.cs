using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Commands.Activities;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetActivities
{
    public class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<ActivityViewModel>>
    {
        private readonly IActivityAsyncRepository repository;
        private readonly ILogger<GetActivitiesHandler> logger;

        public GetActivitiesHandler(
            IActivityAsyncRepository repository,
            ILogger<GetActivitiesHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<IEnumerable<ActivityViewModel>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get activities query.");

            var nodes = await repository.GetActivitiesAsync(request.PageNumber, request.PageSize, request.SortColumn, request.SortDirection);

            logger.LogInformation("Get activities query finished.");

            return nodes.Adapt<IEnumerable<ActivityViewModel>>();
        }
    }
}
