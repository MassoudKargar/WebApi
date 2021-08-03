using Api.Entities;

namespace Api.Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}