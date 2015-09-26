using System.Collections.Generic;
using System.Threading.Tasks;
using InstaSharp.Models;

namespace Instad128000.Core.Common.Interfaces
{
    public interface ISocialNetworkUser
    {
        string UserName { get; set; }

        string UserPassword { get; set; }

        bool Authorize();
    }
}
