using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Queries.GetSegments
{
    public class GetSegmentsHandler : IRequestHandler<GetSegmentsQuery, IEnumerable<SegmentViewModel>>
    {
        private readonly IAsyncRepository<Segment> repository;
        private readonly ILogger<GetSegmentsHandler> logger;

        public GetSegmentsHandler(
            IAsyncRepository<Segment> repository,
            ILogger<GetSegmentsHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<IEnumerable<SegmentViewModel>> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get segments query.");

            var segments = await repository.GetAllAsync();

            logger.LogInformation("Get segments query finished.");

            return segments.Adapt<IEnumerable<SegmentViewModel>>();
        }
    }
}
