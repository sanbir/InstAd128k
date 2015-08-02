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
            if (!context.RequestResults.Any())
            {
                var def = new DataRequestResult()
                {
                    CommentText = "ohohoh",
                    Link = "ohohoh",
                    Type = RequestType.Like,
                    UserId = 12,
                    VictimsId = 13,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ID = Guid.NewGuid(),
                    IsDeleted = false
                };

                context.RequestResults.Add(def);
            }
        }
    }
}
