namespace MapMonitor.Models
{
    public class SqlConfig
    {
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public string ToConnectionString()
        {
            return $"Host={Server};Username={Login};Password={Password};Database=inf_region";
        }
    }
}