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
            #region CharToSymbols
            var charToSymbolRows = new List<CharToSymbol>();
            charToSymbolRows.Add(new CharToSymbol()
            {
                Char = "п",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
                Symbol = "π"
            });
            foreach (var row in charToSymbolRows)
            {
                if (!context.CharToSymbolSet.Any(x => x.Char == row.Char && x.Symbol == row.Symbol))
                {
                    context.CharToSymbolSet.Add(row);
                }
            }
            #endregion

            #region RepeatableChars
            var repeatableChars = new List<RepeatableChars>();
            repeatableChars.Add(new RepeatableChars()
            {
                Char = ":",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = ";",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = ")",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = "(",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = ".",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = ",",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = "$",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = "\\",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = "/",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableChars()
            {
                Char = "|",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            foreach (var row in repeatableChars)
            {
                if (!context.RepeatableCharsSet.Any(x => x.Char == row.Char))
                {
                    context.RepeatableCharsSet.Add(row);
                }
            }
            #endregion  
        }
    }
}
