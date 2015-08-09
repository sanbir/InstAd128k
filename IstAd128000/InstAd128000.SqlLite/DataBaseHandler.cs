using System.Data.SqlServerCe;
using InstAd128000.SqlLite.Properties;

namespace InstAd128000.SqlLite
{

    public class DataBaseHandler
    {
        public DataBaseHandler()
        {
            string connectionString = Settings.Default.dataConnectionString;
            var conn = new SqlCeConnection(connectionString);
            conn.Open();
            //todo: refactoring, создание комманд
        }

    }
}
