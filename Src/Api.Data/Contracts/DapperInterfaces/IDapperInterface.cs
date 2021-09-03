
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Api.Entities;
using Api.Entities.Posts;

namespace Api.Data.Contracts.DapperInterfaces
{
    public interface IDapperInterface<T> where T : class, IEntity
    {
        public IDbConnection Connection { get; }
        Task<IEnumerable<Post>> GetAsync(CancellationToken cancellationToken);
        Task AddAsync(T entity,CancellationToken cancellationToken);
    }
}
