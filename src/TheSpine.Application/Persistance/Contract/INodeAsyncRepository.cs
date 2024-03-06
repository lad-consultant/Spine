using TheSpine.Application.Features.Commands.Nodes;
using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Persistance.Contract
{
    public interface INodeAsyncRepository : IAsyncRepository<Node>
    {
        Task<int> ReorderNodes(List<NodeViewModel> nodes);
        Task<int> DeleteNodesWithIds(List<int> ids);
        Task<bool> UrlExists(int id, string url);
    }
}
