using TheSpine.Core;
using TheSpine.Core.Enums;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Application.Persistance.Contract
{
    public interface IActivityAsyncRepository : IAsyncRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetActivitiesAsync(int pageNumber, int pageSize, string sortColumn, Core.Enums.SortDirection sortDirection);
        Task<IEnumerable<Activity>> GetFilteredActivitiesAsync(string sortColumn, SortDirection sortDirection, string searchTerm);
        Task<int> GetActivityRowsCount();
    }
}
