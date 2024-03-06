using Mapster;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Abstractions;
using TheSpine.Application.Features.Commands.SegmentItems.Manage;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Features.Commands.SegmentItems.AddSegmentItem
{
    internal class ManageSegmentItemHandler : ICommandHandler<ManageSegmentItemCommand, Response>
    {
        private readonly IAsyncRepository<SegmentItem> repository;
        private readonly ILogger<ManageSegmentItemHandler> logger;

        public ManageSegmentItemHandler(
            IAsyncRepository<SegmentItem> repository,
            ILogger<ManageSegmentItemHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Response> Handle(ManageSegmentItemCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling the manage segment item command.");

            var response = new Response();
            var segmentItem = request.SegmentItemViewModel.Adapt<SegmentItem>();

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
                response.Message = request.IsEditing ? $"Segment item {segmentItem.Title} updated successfully" : $"Segment item {segmentItem.Title} created successfully";

                logger.LogInformation("Manage segment item command succeeded.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to manage segment item.");

                response.IsSuccess = false;
                response.Errors.Add(string.Empty, new string[] { ex.Message });
            }
            
            return response;
        }
    }
}
