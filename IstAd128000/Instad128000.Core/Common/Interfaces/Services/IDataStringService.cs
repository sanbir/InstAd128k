using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Models.DataModels;
using Instad128000.Core.Common.Enums;

namespace Instad128000.Core.Common.Interfaces.Services
{
    public interface IDataStringService : ICrudService<DataString>
    {
        bool MatchString(string str, SentenceChanges change);
        List<string> GetSymbolsByString(string str);
    }
}
