using MapMonitor.Models;
using Microsoft.Win32;

namespace MapMonitor.Tools
{
    public static class RegConfig
    {
        public const string RootKey = @"MapMonitor";
        public static SqlConfig Load()
        {
            var hkcu = Registry.CurrentUser;
            var software = hkcu.OpenSubKey("Software");
            var key = software?.OpenSubKey(RootKey);
            if (key != null)
            {
                var sqlConfig = new SqlConfig
                {
                    Server = key.GetValue("Server", null)?.ToString(),
                    Login = key.GetValue("Login", null)?.ToString(),
                    Password = key.GetValue("Password", null)?.ToString(),
                    Database = key.GetValue("Database", null)?.ToString()
                };
                key.Close();
                return sqlConfig;
            }

            return null;
        }

        public static void Save(SqlConfig sqlConfig)
        {
            if (sqlConfig != null)
            {
                var hkcu = Registry.CurrentUser;
                var software = hkcu.OpenSubKey("Software", true);
                if (software != null)
                {
                    var key = software.CreateSubKey(RootKey, true);
                    key.SetValue("Server", sqlConfig.Server);
                    key.SetValue("Login", sqlConfig.Login);
                    key.SetValue("Password", sqlConfig.Password);
                    key.SetValue("Database", sqlConfig.Database);
                    key.Close();
                }
            }
        }
    }
}