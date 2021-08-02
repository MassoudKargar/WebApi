using System.Threading;
using System.Threading.Tasks;

using Api.Entities;

namespace Api.Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    }
}