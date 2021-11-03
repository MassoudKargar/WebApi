
using Api.Common;

using Ccms.Common;

namespace Api.Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
