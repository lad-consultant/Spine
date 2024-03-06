using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Application.Features.Commands.Segments.Delete
{
    public class DeleteSegmentHandler : IRequestHandler<DeleteSegmentCommand, Response>
    {
        private readonly ISegmentAsyncRepository segmentRepository;
        private readonly ILogger<DeleteSegmentHandler> logger;

        public DeleteSegmentHandler(
            ISegmentAsyncRepository segmentRepository,
            ILogger<DeleteSegmentHandler> logger)
        {
            this.segmentRepository = segmentRepository;
            this.logger = logger;
        }

        public async Task<Response> Handle(DeleteSegmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the delete segment command.");

            var response = new Response();
            var segment = request.SegmentToDelete.Adapt<Segment>();

            try
            {
                var segmentItems = await segmentRepository.DeleteIncludingRelations(request.SegmentToDelete.Id);

                response.IsSuccess = true;
                response.Message = $"Category {segment.Title} deleted successfully";

                logger.LogInformation("Delete segment command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete segment.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
