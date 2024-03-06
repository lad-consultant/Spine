using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class SegmentItemAsyncRepository : BaseAsyncRepository<SegmentItem>, ISegmentItemAsyncRepository
    {
        private readonly IItemDetailedInfoAsyncRepository itemInfoRepository;
        private readonly ILogger<SegmentItemAsyncRepository> logger;

        public SegmentItemAsyncRepository(
            IConfiguration configuration,
            IItemDetailedInfoAsyncRepository itemInfoRepository,
            ILogger<SegmentItemAsyncRepository> logger) : base(configuration, logger)
        {
            this.itemInfoRepository = itemInfoRepository;
            this.logger = logger;
        }

        public async Task<int> DeleteIncludingRelations(int segmentItemId)
        {
            logger.LogInformation("Deleting item and his infos.");

            await itemInfoRepository.DeleteBySegmentItemId(segmentItemId);

            using var connection = CreateConnection();

            return await connection.ExecuteAsync("DELETE FROM SegmentItems WHERE Id = @segmentItemId", new { segmentItemId });
        }

        public async Task<IEnumerable<SegmentItem>?> GetByLeafNodeIdAsync(int id)
        {
            logger.LogInformation("Getting the items by node id.");

            using var connection = CreateConnection();
            return await connection.QueryAsync<SegmentItem>("SELECT * FROM SegmentItems WHERE NodeId = @id", new { id });
        }
    }
}
