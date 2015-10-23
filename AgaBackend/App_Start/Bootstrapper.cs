using System.Reflection;
using System.Web.Http;
using AgaBackend.Controllers;
using AgaBackend.Datasource;
using AgaBackend.Models;
using AgaBackend.Services;
using Autofac;
using Autofac.Integration.WebApi;

namespace AgaBackend
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ConfigureAutofacContainer();
        }

        public static void ConfigureAutofacContainer()
        {
            var webApiContainerBuilder = new ContainerBuilder();
            ConfigureWebApiContainer(webApiContainerBuilder);
        }

        public static void ConfigureWebApiContainer(ContainerBuilder containerBuilder)
        {
            // Register ApiControllers
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterType<SimCarService>().As<ISimCarService>().InstancePerRequest();

            containerBuilder.RegisterType<StatsService>().As<IStatsService>().InstancePerRequest();
         
            containerBuilder.RegisterType<AgaMongoClient>().As<IAgaMongoClient>().SingleInstance();
            containerBuilder.RegisterType<AgaMongoServer>().As<IAgaMongoServer>().SingleInstance();

            containerBuilder.RegisterType<MongoDataSource<Snapshot>>().As<IMongoDatasource<Snapshot>>().WithParameter("datasourceParams",new DatasourceParams () {CollectionName = "snapshot", DatabaseName = "telemetry"}).InstancePerRequest();
            containerBuilder.RegisterType<MongoDataSource<RouteModel>>().As<IMongoDatasource<RouteModel>>().WithParameter("datasourceParams", new DatasourceParams() { CollectionName = "routes", DatabaseName = "telemetry" }).InstancePerRequest();

            IContainer container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}