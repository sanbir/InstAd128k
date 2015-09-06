using System.Collections.Generic;
using System.Threading.Tasks;
using InstaSharp.Models;

namespace Instad128000.Core.Common.Interfaces
{
    interface ISocialNetworkUser<TContactModel>
    {
        string UserName { get; set; }

        string UserPassword { get; set; }

        bool Authorize();

        Task<List<TContactModel>> GetContactsListOf(string userName);

        Task<List<TContactModel>> AddToContactsAllContactsOf(string userName);
    }
}
