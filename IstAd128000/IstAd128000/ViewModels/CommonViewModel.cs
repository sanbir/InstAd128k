using Instad128000.Core.Common.Interfaces.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels
{
    public class CommonViewModel
    {
        [Dependency]
        public IRequestService RequestService { get; set; }
        [Dependency]
        public IDataStringService DataStringService { get; set; }
    }
}
