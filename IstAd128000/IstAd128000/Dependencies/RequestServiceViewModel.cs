using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Services;

namespace InstAd128000.Dependencies
{
    public class RequestServiceViewModel
    {
        public IRequestService RequestService { get; set; }

        public RequestServiceViewModel(IRequestService rs)
        {
            RequestService = rs;
        }

    }
}
