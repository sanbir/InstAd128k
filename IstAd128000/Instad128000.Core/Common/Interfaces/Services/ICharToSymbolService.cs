using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Models.DataModels;

namespace Instad128000.Core.Common.Interfaces.Services
{
    public interface ICharToSymbolService : ICrudService<CharToSymbol>, IMatchChar
    {
        List<string> GetSymbolsByChar(char chr);
    }
}
