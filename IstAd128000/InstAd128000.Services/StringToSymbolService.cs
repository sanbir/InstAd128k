using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models.DataModels;

namespace InstAd128000.Services
{
    public class StringToSymbolService: CrudService<StringToSymbol>, IStringToSymbolService
    {
        public StringToSymbolService(IRepository<StringToSymbol> repo) : base(repo)
        {
        }

        public bool MatchString(string strToMatch)
        {
            return this.Repo.Where(x => x.String == strToMatch).Any();
        }

        public List<string> GetSymbolsByString(string str)
        {
            return this.Repo.Where(x => x.String == str).Select(x => x.Symbol).ToList();
        }
    }
}
