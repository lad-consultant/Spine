using TheSpine.Core;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Persistance.Contract
{
	public interface IItemDetailedInfoAsyncRepository : IAsyncRepository<ItemDetailedInfo>
	{
		Task<IEnumerable<ItemDetailedInfo>> GetBySegmentItemId(int segmentItemId);
		Task<IEnumerable<ItemDetailedInfo>> DeleteBySegmentItemId(int segmentItemId);
		Task<IEnumerable<SearchItem>> Search(string input);
	}
}
