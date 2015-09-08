using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models.DataModels;
using Instad128000.Core.Common.Enums;

namespace InstAd128000.Services
{
    public class DataStringService : CrudService<DataString>, IDataStringService
    {
        public DataStringService(IRepository<DataString> repo) : base(repo)
        {
        }

        public bool MatchString(string strToMatch, SentenceChanges change)
        {
            switch (change)
            {
                case (SentenceChanges.CharToSymbol):
                    return this.Repo.Where(x => x.String == strToMatch && x.StringToSymbolSymbol != null && x.StringToSymbolSymbol != "").Any();
                case (SentenceChanges.RepeatChar):
                    return this.Repo.Where(x => x.String == strToMatch && x.IsRepeatable).Any();
                default:
                    throw new Exception("Add InstAd128000.Services.DataStringService.MatchString handler for " + change);
            }
        }

        public List<string> GetSymbolsByString(string str)
        {
            return this.Repo.Where(x => x.String == str).Select(x => x.StringToSymbolSymbol).ToList();
        }
    }
}
