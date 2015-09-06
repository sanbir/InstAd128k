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
    public class RepeatableStringsService: CrudService<RepeatableStrings>, IRepeatableStringsService
    {
        public RepeatableStringsService(IRepository<RepeatableStrings> repo) : base(repo)
        {
        }

        public bool MatchString(string charToMatch)
        {
            var toString = charToMatch;
            return this.Repo.Where(x => x.String == toString).Any();
        }
    }
}
