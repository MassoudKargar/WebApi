using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts.DapperInterfaces;
using Api.Entities;

using Dapper;

using Microsoft.Extensions.Configuration;

namespace Api.Data.Repositories.DapperServices
{
    public class DapperService<T> : IDapperInterface<T> where T : class, IEntity
    {
        public DapperService(IConfiguration configuration, ApplicationDbContext context)
        {
            Configuration = configuration;
            Context = context;
        }

        private IConfiguration Configuration { get; }
        private ApplicationDbContext Context { get; }

        public async Task AddWithSpAsync(T entity, CancellationToken cancellationToken)
        {
            string quary = "SP_Post_Insert";
            string connectionString = Configuration.GetConnectionString("SqlServer");
            using IDbConnection db = new SqlConnection(connectionString);
            await db.ExecuteAsync(quary, entity, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
        {
            //TODO:add connection string
            string connectionString = Configuration.GetConnectionString("SqlServer");
            //TODO:Sql quary or stord procedore(sp)
            var quaryPostTest = "SP_Post_GetAll";

            using IDbConnection db = new SqlConnection(connectionString);
            var result = await db.QueryAsync<T>(quaryPostTest, cancellationToken);
            return result;

        }
    }
}
