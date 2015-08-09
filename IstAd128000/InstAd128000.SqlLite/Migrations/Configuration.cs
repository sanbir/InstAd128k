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
            var charToSymbolRows = new List<StringToSymbol>();
            charToSymbolRows.Add(new StringToSymbol()
            {
                String = "п",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
                Symbol = "π"
            });
            foreach (var row in charToSymbolRows)
            {
                if (!context.CharToSymbolSet.Any(x => x.String == row.String && x.Symbol == row.Symbol))
                {
                    context.CharToSymbolSet.Add(row);
                }
            }
            #endregion

            #region RepeatableChars
            var repeatableChars = new List<RepeatableStrings>();
            repeatableChars.Add(new RepeatableStrings()
            {
                String = ":",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = ";",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = ")",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = "(",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = ".",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = ",",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = "$",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = "\\",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = "/",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            repeatableChars.Add(new RepeatableStrings()
            {
                String = "|",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            foreach (var row in repeatableChars)
            {
                if (!context.RepeatableCharsSet.Any(x => x.String == row.String))
                {
                    context.RepeatableCharsSet.Add(row);
                }
            }
            #endregion  

            #region AddableStrings
            var addableCharsRows = new List<AddableStrings>();
            addableCharsRows.Add(new AddableStrings()
            {
                String = ")",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            addableCharsRows.Add(new AddableStrings()
            {
                String = "(",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            addableCharsRows.Add(new AddableStrings()
            {
                String = "!",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            addableCharsRows.Add(new AddableStrings()
            {
                String = "D",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            addableCharsRows.Add(new AddableStrings()
            {
                String = ":P",
                CreateDate = DateTime.Now,
                ID = Guid.NewGuid(),
                IsDeleted = false,
                ModifyDate = DateTime.Now,
            });
            foreach (var row in addableCharsRows)
            {
                if (!context.AddableCharsSet.Any(x => x.String == row.String))
                {
                    context.AddableCharsSet.Add(row);
                }
            }
            #endregion
        }
    }
}
