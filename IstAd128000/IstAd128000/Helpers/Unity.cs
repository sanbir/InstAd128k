using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Logger;
using InstAd128000.Services;
using InstAd128000.SqlLite;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.Helpers
{
    public static class Unity
    {
        private static IUnityContainer _container { get; set; }
        public static IUnityContainer Container
        {
            get
            {
                var cont = _container ?? (_container = new UnityContainer());

                _container.RegisterType<IDbContextFactory, DbContextFactory>();
                _container.RegisterType<ILogger, Log4NetLogger>();
                _container.RegisterType(typeof(ICrudService<>), typeof(CrudService<>));
                _container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
                _container.RegisterType<IRequestService, RequestService>();
                _container.RegisterType<IDataStringService, DataStringService>();

                return cont;
            }
        }
    }
}
