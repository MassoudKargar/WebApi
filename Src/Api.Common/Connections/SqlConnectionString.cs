namespace Ccms.Common.Connections
{
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlConnectionString : ISingletonDependency, ISqlConnection
    {
        public SqlConnectionString(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        IDbConnection ISqlConnection.GetDbConnection(string databaseName = null)
        {
            string connection = databaseName switch
            {
                not null => Configuration.GetConnectionString("OtherServer").Replace("DBNAME", "prj" + databaseName),
                _ => Configuration.GetConnectionString("CommandServer"),
            };
            return new SqlConnection(connection);
        }
    }
}
