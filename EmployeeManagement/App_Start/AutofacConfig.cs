using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using System.Reflection;
using System.Web.Http;

namespace EmployeeManagement.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            var config = new MapperConfiguration( cfg =>
            {
                cfg.AddProfile(new EmployeeMappingProfile());
            });

            builder.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<EmployeeContext>()
              .InstancePerRequest();

            builder.RegisterType<EmployeeRepository>()
              .As<IEmployeeRepository>()
              .InstancePerRequest();
        }
    }
}