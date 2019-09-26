using MapMonitor.inf_regionDataSetTableAdapters;
using MapMonitor.Models;
using Npgsql;

namespace MapMonitor.Logic
{
    public class RepositoryDb
    {
        private SqlConfig _sqlConfig;
        private NpgsqlConnection _connection;

        public RepositoryDb()
        {
        }
        public void Connect(SqlConfig sqlConfig)
        {
            _sqlConfig = sqlConfig;
            _connection = new NpgsqlConnection(sqlConfig.ToConnectionString());

        }

        public void GetLayers(string groupName)
        {
            var query = "SELECT ti.* FROM sys_scheme.table_info ti " +
                "LEFT JOIN inf_region.sys_scheme.table_groups_table tb ON ti.id = tb.id_table " +
                "LEFT JOIN inf_region.sys_scheme.table_groups gr ON tb.id_group = gr.id " +
                "WHERE gr.name_group = '@groupName' " +
                "ORDER BY tb.order_num";

            using (var cmd = new NpgsqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("groupName", groupName);
                var reader = cmd.ExecuteReader();
                //reader.
            }

            var tableAdapterManager = new TableAdapterManager();
            
        }
    }
}