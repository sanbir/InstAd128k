using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.SqlLite
{
    public class DbContextFactory : IDbContextFactory
    {
        public DbContext GetContext()
        {
            return new Db();
        }
    }
}
