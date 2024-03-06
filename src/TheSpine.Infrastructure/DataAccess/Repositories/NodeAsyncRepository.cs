using Dapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class NodeAsyncRepository : BaseAsyncRepository<Node>, INodeAsyncRepository
    {
        private readonly ILogger<NodeAsyncRepository> logger;

        public NodeAsyncRepository(
            IConfiguration configuration,
            ILogger<NodeAsyncRepository> logger) : base(configuration, logger)
        {
            this.logger = logger;
        }

        public async Task<int> DeleteNodesWithIds(List<int> ids)
        {
            logger.LogInformation("Deleting nodes with specified ids.");

            using var connection = CreateConnection();

            return await connection.ExecuteAsync("DELETE FROM Nodes WHERE Id IN @ids", new { ids });
        }

        public async Task<bool> UrlExists(int id, string url)
        {
            using var connection = CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<bool>("SELECT 1 FROM Nodes WHERE UniqueUrl = @url AND Id <> @id", new { id, url });
        }

        public async Task<int> ReorderNodes(List<NodeViewModel> nodes)
        {
            logger.LogInformation("Reordering nodes.");

            using var connection = CreateConnection();

            return await connection.ExecuteAsync("UPDATE Nodes SET OrderingIndex=@OrderingIndex WHERE Id=@Id", param: nodes);
        }
    }
}
