using System.Threading.Tasks;

using Api.Entities;

namespace Api.Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}