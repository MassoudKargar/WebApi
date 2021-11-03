using System.Data;

namespace Ccms.Common.Connections
{
    public interface ISqlConnection
    {
        IDbConnection GetDbConnection(string databaseName = null);
    }
}