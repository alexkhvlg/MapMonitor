using Npgsql;
using System.Data.Entity;

namespace MapMonitor.Logic
{
    class NpgSqlConfiguration : DbConfiguration
    {
        public NpgSqlConfiguration()
        {
            var name = "Npgsql";
            SetProviderFactory(name, NpgsqlFactory.Instance);
            SetProviderServices(name, NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }
}