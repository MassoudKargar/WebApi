
using Api.Common;

namespace Api.Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
