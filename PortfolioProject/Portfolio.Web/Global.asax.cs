using Autofac;
using Autofac.Integration.Mvc;
using Portfolio.DB;
using Portfolio.Repository.Pictures;
using Portfolio.Repository.Users;
using Portfolio.Service.Pictures;
using Portfolio.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Portfolio.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<PicturesService>().As<IPicturesService>();
            builder.RegisterType<PicturesRepository>().As<IPicturesRepository>();
            builder.RegisterType<PortfolioDbContext>().As<PortfolioDbContext>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
