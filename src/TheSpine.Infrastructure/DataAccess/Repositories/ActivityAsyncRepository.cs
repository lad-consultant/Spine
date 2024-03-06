using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Core;
using TheSpine.Core.Enums;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class ActivityAsyncRepository : BaseAsyncRepository<Activity>, IActivityAsyncRepository
    {
        private readonly ILogger<ActivityAsyncRepository> logger;

        public ActivityAsyncRepository(
            IConfiguration configuration,
            ILogger<ActivityAsyncRepository> logger) : base(configuration, logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync(int pageNumber, int pageSize, string sortColumn, SortDirection sortDirection)
        {
            logger.LogInformation("Getting activities.");

            if (string.IsNullOrWhiteSpace(sortColumn) ||
                !typeof(Activity).GetProperties().Any(x => x.Name == sortColumn))
            {
                sortColumn = nameof(Activity.Timestamp);
            }

            using var connection = CreateConnection();

            return await connection.QueryAsync<Activity>(
                "SELECT * FROM Activitys " +
                "ORDER BY " + sortColumn + " " + sortDirection.GetSortString() +
                " OFFSET @offset " +
                "ROWS FETCH NEXT @pageSize ROWS ONLY",
                new { offset = pageNumber * pageSize, pageSize, });
        }

        public async Task<IEnumerable<Activity>> GetFilteredActivitiesAsync(string sortColumn, SortDirection sortDirection, string searchTerm)
        {
            logger.LogInformation("Getting activities.");

            if (string.IsNullOrWhiteSpace(sortColumn) ||
                !typeof(Activity).GetProperties().Any(x => x.Name == sortColumn))
            {
                sortColumn = nameof(Activity.Timestamp);
            }

            using var connection = CreateConnection();

            string searchQuery = string.Empty;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Trim leading, trailing and consecutive spaces in the string.
                searchTerm = searchTerm.Trim();
                searchTerm = Regex.Replace(searchTerm, @"\s+", " ");

                searchQuery = "WHERE UserId LIKE @searchTerm OR Description LIKE @searchTerm ";
            }

            return await connection.QueryAsync<Activity>(
                "SELECT * FROM Activitys " +
                searchQuery +
                "ORDER BY " + sortColumn + " " + sortDirection.GetSortString(),
                new { searchTerm = $"%{searchTerm}%" });
        }

        public async Task<int> GetActivityRowsCount()
        {
            logger.LogInformation("Getting the row count for the Activitys table.");

            using var connection = CreateConnection();

            return await connection.QuerySingleAsync<int>(
                "SELECT i.rowcnt " +
                "FROM sys.sysindexes i WITH(NOLOCK) " +
                "WHERE i.indid in (0, 1) and OBJECT_NAME(i.id) = 'Activitys' " +
                "ORDER BY i.rowcnt desc");
        }
    }
}
