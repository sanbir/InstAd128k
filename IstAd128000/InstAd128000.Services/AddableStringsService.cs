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
    public class AddableStringsService : CrudService<AddableStrings>, IAddableStringsService
    {
        public AddableStringsService(IRepository<AddableStrings> repo) : base(repo)
        {
        }
    }
}
