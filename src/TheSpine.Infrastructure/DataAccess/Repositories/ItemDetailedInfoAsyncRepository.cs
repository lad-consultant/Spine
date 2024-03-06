using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
	public class ItemDetailedInfoAsyncRepository : BaseAsyncRepository<ItemDetailedInfo>, IItemDetailedInfoAsyncRepository
	{
        private readonly ILogger<ItemDetailedInfoAsyncRepository> logger;

        public ItemDetailedInfoAsyncRepository(
			IConfiguration configuration,
			ILogger<ItemDetailedInfoAsyncRepository> logger) : base(configuration, logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<ItemDetailedInfo>> DeleteBySegmentItemId(int segmentItemId)
		{
			logger.LogInformation("Deleting item infos by segment item id.");

            using var connection = CreateConnection();
			return await connection.QueryAsync<ItemDetailedInfo>("DELETE FROM ItemDetailedInfos WHERE SegmentItemId = @segmentItemId", new { segmentItemId });
		}

		public async Task<IEnumerable<ItemDetailedInfo>> GetBySegmentItemId(int segmentItemId)
		{
            logger.LogInformation("Getting items infos by segment item id.");

            using var connection = CreateConnection();
			return await connection.QueryAsync<ItemDetailedInfo>("SELECT * FROM ItemDetailedInfos WHERE SegmentItemId = @segmentItemId", new { segmentItemId });
		}

		public async Task<IEnumerable<SearchItem>> Search(string input)
		{
            logger.LogInformation("Searching for item infos.");

			var itemDetails = @"SELECT ItemDetailedInfos.Title, ItemDetailedInfos.TextContent AS Description, CONCAT('/nodes/', Nodes.Id, '/segments/', SegmentItems.Id) as Url FROM ItemDetailedInfos 
								JOIN SegmentItems ON SegmentItems.Id = ItemDetailedInfos.SegmentItemId
								JOIN Nodes ON Nodes.Id = SegmentItems.NodeId
								WHERE ItemDetailedInfos.Title LIKE @input OR ItemDetailedInfos.TextContent LIKE @input";
			var nodes = "SELECT Title, '' AS Description, CONCAT('/nodes/', Id) as Url FROM Nodes WHERE Title LIKE @input";
			var segments = @"SELECT SegmentItems.Title, Notes as Description, CONCAT('/nodes/', Nodes.Id) as Url FROM SegmentItems 
							 JOIN Nodes ON SegmentItems.NodeId = Nodes.Id
							 WHERE SegmentItems.Title LIKE @input OR Notes LIKE @input";

            using var connection = CreateConnection();
			var items = await connection.QueryAsync<SearchItem>($"{itemDetails} UNION {nodes} UNION {segments}", new { input = $"%{input.ToLowerInvariant()}%" });

			return items;
		}
	}
}
