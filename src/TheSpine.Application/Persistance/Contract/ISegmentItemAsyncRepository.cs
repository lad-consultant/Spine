using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Persistance.Contract
{
    public interface ISegmentItemAsyncRepository : IAsyncRepository<SegmentItem>
    {
        Task<IEnumerable<SegmentItem>?> GetByLeafNodeIdAsync(int id);
        Task<int> DeleteIncludingRelations(int segmentItemId);
    }
}
