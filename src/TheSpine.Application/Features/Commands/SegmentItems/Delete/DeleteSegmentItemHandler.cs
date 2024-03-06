using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Application.Features.Commands.SegmentItems.DeleteSegmentItem
{
    public class DeleteSegmentItemHandler : IRequestHandler<DeleteSegmentItemCommand, Response>
    {
        private readonly ISegmentItemAsyncRepository _segmentItemrepository;
        private readonly ILogger<DeleteSegmentItemHandler> logger;

        public DeleteSegmentItemHandler(
            ISegmentItemAsyncRepository segmentItemrepository,
            ILogger<DeleteSegmentItemHandler> logger)
        {
            _segmentItemrepository = segmentItemrepository;
            this.logger = logger;
        }

        public async Task<Response> Handle(DeleteSegmentItemCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the delete segment item command.");

            var response = new Response();
            var segmentItem = request.SegmentItemToDelete.Adapt<SegmentItem>();

            try
            {
                await _segmentItemrepository.DeleteIncludingRelations(segmentItem.Id);

                response.IsSuccess = true;
                response.Message = $"Item {segmentItem.Title} deleted successfully";

                logger.LogInformation("Delete segment item command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete segment item.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
