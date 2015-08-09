using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Logger;
using Instad128000.Core.Helpers;
using InstAd128000.Services;
using InstAd128000.SqlLite;
using Microsoft.Practices.Unity;

namespace InstAd128000.TestApplication
{
    class Program
    {
        static IUnityContainer _container;

        static void Main(string[] args)
        {
            LoadContainer();

            var srv = _container.Resolve<ICharToSymbolService>();
            
        }

        private static void LoadContainer()
        {
            _container = new UnityContainer();

            _container.RegisterType<IDbContextFactory, DbContextFactory>();
            _container.RegisterType(typeof(ICrudService<>), typeof(CrudService<>));
            _container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            _container.RegisterType<ICharToSymbolService, CharToSymbolService>();
            _container.RegisterType<IRequestService, RequestService>();
        }
    }
}
