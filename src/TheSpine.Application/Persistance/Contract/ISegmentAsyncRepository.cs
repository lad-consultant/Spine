using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Persistance.Contract
{
    public interface ISegmentAsyncRepository : IAsyncRepository<Segment>
    {
        Task<int> DeleteIncludingRelations(int segmentId);
    }
}
