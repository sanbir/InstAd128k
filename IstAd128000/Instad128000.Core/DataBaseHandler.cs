using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
namespace Instad128000.Core
{

    public class DataBaseHandler
    {
        public DataBaseHandler()
        {
            string _ConnectionString = "Data Source=data.sdf;  Persist Security Info=False;";
            SqlCeConnection _Conn = new SqlCeConnection(_ConnectionString);
            _Conn.Open();
            //todo: refactoring, создание комманд
        }

    }
}
