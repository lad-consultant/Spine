using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Features.Commands.SegmentItems;
using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Queries.GetSegmentItemsForLeafNode
{
    public class GetSegmentItemsForLeafNodeHandler : IRequestHandler<GetSegmentItemsForLeafNodeQuery, IEnumerable<SegmentItemViewModel>>
    {
        private readonly ISegmentItemAsyncRepository segmentItemRepository;
        private readonly IAsyncRepository<Segment> segmentRepository;
        private readonly ILogger<GetSegmentItemsForLeafNodeHandler> logger;

        public GetSegmentItemsForLeafNodeHandler(
            ISegmentItemAsyncRepository segmentItemRepository,
            IAsyncRepository<Segment> segmentRepository,
            ILogger<GetSegmentItemsForLeafNodeHandler> logger)
        {
            this.segmentItemRepository = segmentItemRepository;
            this.segmentRepository = segmentRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<SegmentItemViewModel>> Handle(GetSegmentItemsForLeafNodeQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the get segment items for leaf node query.");

            var segmentItems = await segmentItemRepository.GetByLeafNodeIdAsync(request.ViewModel.Id);

            if (segmentItems != null) 
            {
                Dictionary<int, Segment> segmentCash = new Dictionary<int, Segment>(10);

                List<SegmentItemViewModel> viewModels = new List<SegmentItemViewModel>(10);

                foreach (var segmentItem in segmentItems)
                {
                    Segment currentSegment= null;

                    if (!segmentCash.TryGetValue(segmentItem.SegmentId, out currentSegment))
                    {
                        currentSegment = await segmentRepository.GetByIdAsync(segmentItem.SegmentId);
                        segmentCash[segmentItem.SegmentId] = currentSegment;
                    }

                    var currentViewModel = segmentItem.Adapt<SegmentItemViewModel>();
                    currentViewModel.Segment = currentSegment.Adapt<SegmentViewModel>();
                    viewModels.Add(currentViewModel);
                }

                logger.LogInformation("Get segment items for leaf node query is finished.");

                return viewModels;
            }

            logger.LogInformation("No segment items found for specified leaf node id.");

            return Enumerable.Empty<SegmentItemViewModel>();
        }
    }
}
