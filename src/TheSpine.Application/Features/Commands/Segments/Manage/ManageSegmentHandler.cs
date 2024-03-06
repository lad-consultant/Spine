using Mapster;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Commands.Segments.Manage
{
    public class ManageSegmentHandler : ICommandHandler<ManageSegmentCommand, Response>
    {
        private readonly IAsyncRepository<Segment> repository;
        private readonly ILogger<ManageSegmentHandler> logger;

        public ManageSegmentHandler(
            IAsyncRepository<Segment> repository,
            ILogger<ManageSegmentHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(ManageSegmentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the manage segment command.");

            var response = new Response();
            var segmentItem = request.SegmentViewModel.Adapt<Segment>();

            try
            {
                if (request.IsEditing)
                {
                    await repository.UpdateAsync(segmentItem);
                }
                else
                {
                    await repository.AddAsync(segmentItem);
                }

                response.IsSuccess = true;
                response.Message = request.IsEditing ? $"Category {segmentItem.Title} updated successfully" : $"Category {segmentItem.Title} created successfully";

                logger.LogInformation("Manage segment command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to manage segment.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }

            return response;
        }
    }
}
