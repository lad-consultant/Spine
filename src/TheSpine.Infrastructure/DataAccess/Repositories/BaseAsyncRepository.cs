using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheSpine.Core.Common;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class BaseAsyncRepository<T> : IAsyncRepository<T> where T : IdentifiableEntity
    {
        protected readonly IConfiguration _configuration;
        private readonly ILogger<BaseAsyncRepository<T>> logger;
        protected readonly string _tableName;

        public BaseAsyncRepository(
            IConfiguration configuration,
            ILogger<BaseAsyncRepository<T>> logger)
        {
            _configuration = configuration;
            this.logger = logger;
            _tableName = $"{typeof(T).Name}s";
        }

        public async Task<int> AddAsync(T entity)
        {
            logger.LogInformation("Adding entity");

            using var connection = CreateConnection();
            return await connection.InsertAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            logger.LogInformation("Deleting entity");

            using var connection = CreateConnection();
            await connection.DeleteAsync<T>(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            logger.LogInformation("Getting all entities.");

            using var connection = CreateConnection();
            return await connection.GetAllAsync<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting entity by id.");

            using var connection = CreateConnection();
            return await connection.GetAsync<T>(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            logger.LogInformation("Updating entity.");

            using var connection = CreateConnection();
            await connection.UpdateAsync(entity);

            return entity;
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("Default"));
        }

    }
}
