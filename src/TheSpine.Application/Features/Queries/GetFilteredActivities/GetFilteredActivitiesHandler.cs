using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Commands.Activities;
using TheSpine.Application.Persistance.Contract;

namespace TheSpine.Application.Features.Queries.GetFilteredActivities
{
    public class GetFilteredActivitiesHandler : IRequestHandler<GetFilteredActivitiesQuery, IEnumerable<ActivityViewModel>>
    {
        private readonly IActivityAsyncRepository repository;
        private readonly ILogger<GetFilteredActivitiesHandler> logger;

        public GetFilteredActivitiesHandler(
            IActivityAsyncRepository repository,
            ILogger<GetFilteredActivitiesHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<IEnumerable<ActivityViewModel>> Handle(GetFilteredActivitiesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get filtered activities query.");

            var nodes = await repository.GetFilteredActivitiesAsync(request.SortColumn, request.SortDirection, request.SearchTerm);

            logger.LogInformation("Get filtered activities query finished.");

            return nodes.Adapt<IEnumerable<ActivityViewModel>>();
        }
    }
}
