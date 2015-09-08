using System.Collections.Generic;
using System.Data;
using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Models.DataModels;

namespace InstAd128000.SqlLite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<InstAd128000.SqlLite.Db>
    { 
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(InstAd128000.SqlLite.Db context)
        {
            var rows = new List<DataString>();
            rows.Add(new DataString(){String = "п",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = "π",IsAddable = false,IsRepeatable = false});
            rows.Add(new DataString(){String = ")",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = true,IsRepeatable = true});
            rows.Add(new DataString(){String = "(",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = true,IsRepeatable = true});
            rows.Add(new DataString(){String = "!",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = true,IsRepeatable = false});
            rows.Add(new DataString(){String = "D",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = true,IsRepeatable = false});
            rows.Add(new DataString(){String = ":P",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = true,IsRepeatable = false});
            rows.Add(new DataString(){String = ":",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now,StringToSymbolSymbol = null,IsAddable = false,IsRepeatable = true});
            rows.Add(new DataString(){String = ";",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true});
            rows.Add(new DataString(){String = ".",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true });
            rows.Add(new DataString(){String = ",",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true });
            rows.Add(new DataString(){String = "$",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true });
            rows.Add(new DataString(){String = "\\",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true });
            rows.Add(new DataString(){String = "/",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true });
            rows.Add(new DataString(){String = "|",CreateDate = DateTime.Now,ID = Guid.NewGuid(),IsDeleted = false,ModifyDate = DateTime.Now, StringToSymbolSymbol = null, IsAddable = false, IsRepeatable = true }) ;
          
            foreach (var row in rows)
            {
                var existent = context.DataStrings.FirstOrDefault(x => x.String == row.String);
                if (existent != null)
                {
                    existent.StringToSymbolSymbol = row.StringToSymbolSymbol;
                    existent.IsAddable = row.IsAddable;
                    existent.IsRepeatable = row.IsRepeatable;
                    context.SaveChanges();
                }
                else
                {
                    context.DataStrings.Add(row);
                    context.SaveChanges();
                }

            }
        }
    }
}
