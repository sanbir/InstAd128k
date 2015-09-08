using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Models.DataModels;
using InstAd128000.SqlLite.Migrations;

namespace InstAd128000.SqlLite
{
    public class Db : DbContext
    {
        public Db() : base("DataContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());
        }

        public DbSet<DataRequestResult> RequestResults { get; set; }
        public DbSet<DataString> DataStrings { get; set; }
    }
}
