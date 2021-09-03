using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts.DapperInterfaces;
using Api.Entities;
using Api.Entities.Posts;

using Dapper;

using Microsoft.Extensions.Configuration;

namespace Api.Data.Repositories.DapperServices
{
    public class DapperService<T> : IDapperInterface<T> where T : class, IEntity
    {
        protected readonly IConfiguration _config;

        public DapperService(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("SqlServer"));
            }
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            using IDbConnection db = Connection;
            await db.ExecuteAsync("SP_Post_Insert", entity, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Post>> GetAsync(CancellationToken cancellationToken)
        {
            using IDbConnection db = Connection;
            var result = await db.QueryAsync<Post>("SP_Post_GetAll", cancellationToken);
            return result;

        }
    }
}
