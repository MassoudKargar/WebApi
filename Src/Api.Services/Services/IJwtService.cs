using Api.Entities;

using Ccms.Common;

using System.Threading.Tasks;

namespace Api.Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}