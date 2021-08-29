
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Entities;

namespace Api.Data.Contracts.DapperInterfaces
{
    public interface IDapperInterface<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken);
        Task AddWithSpAsync(T entity,CancellationToken cancellationToken);
    }
}
