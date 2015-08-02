using System;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Logger;
using InstAd128000.Services;
using InstAd128000.SqlLite;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace InstAd128000
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType(typeof(IDbContextFactory), typeof(DbContextFactory));
            container.RegisterType(typeof(ILogger), typeof(Log4NetLogger));
            container.RegisterType(typeof(ICrudService<>), typeof(CrudService<>));
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}