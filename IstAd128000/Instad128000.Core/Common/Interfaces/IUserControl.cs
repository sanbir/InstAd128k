using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace Instad128000.Core.Common.Interfaces
{
    public interface IDBInteractive
    {
        IRequestService RequestService { get; set; }
        IDataStringService DataStringService { get; set; }
    }
}
