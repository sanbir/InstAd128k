using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models.DataModels;

namespace InstAd128000.Services
{
    public class CharToSymbolService: CrudService<CharToSymbol>, ICharToSymbolService
    {
        public CharToSymbolService(IRepository<CharToSymbol> repo) : base(repo)
        {
        }
    }
}
