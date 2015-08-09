using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Models.DataModels;

namespace Instad128000.Core.Common.Interfaces.Services
{
    public interface IRequestService : ICrudService<DataRequestResult>
    {
    }
}
