using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class SegmentAsyncRepository : BaseAsyncRepository<Segment>, ISegmentAsyncRepository
    {
        private readonly ISegmentItemAsyncRepository segmentItemRepository;
        private readonly ILogger<SegmentAsyncRepository> logger;

        public SegmentAsyncRepository(
            IConfiguration configuration,
            ISegmentItemAsyncRepository segmentItemRepository,
            ILogger<SegmentAsyncRepository> logger) : base(configuration, logger)
        {
            this.segmentItemRepository = segmentItemRepository;
            this.logger = logger;
        }

        public async Task<int> DeleteIncludingRelations(int segmentId)
        {
            logger.LogInformation("Deleting the segment including related items.");

            using var connection = CreateConnection();

            var segmentItems = await connection.QueryAsync<SegmentItem>("SELECT * FROM SegmentItems WHERE SegmentId = @segmentId", new { segmentId });

            foreach (var segmentItem in segmentItems)
            {
                await segmentItemRepository.DeleteIncludingRelations(segmentItem.Id);
            }

            return await connection.ExecuteAsync("DELETE FROM Segments WHERE Id = @segmentId", new { segmentId });
        }
    }
}
