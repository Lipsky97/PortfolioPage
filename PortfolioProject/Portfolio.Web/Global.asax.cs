using Autofac;
using Autofac.Integration.Mvc;
using Portfolio.DB;
using Portfolio.Repository.CVs;
using Portfolio.Repository.Pictures;
using Portfolio.Repository.PortfolioView;
using Portfolio.Repository.Users;
using Portfolio.Service.CVs;
using Portfolio.Service.Pictures;
using Portfolio.Service.PortfolioView;
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
            builder.RegisterType<CVsService>().As<ICVsService>();
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<PicturesService>().As<IPicturesService>();
            builder.RegisterType<PortfolioViewService>().As<IPortfolioViewService>();
            builder.RegisterType<PortfolioViewRepository>().As<IPortfolioViewRepository>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<CVsRepository>().As<ICVsRepository>();
            builder.RegisterType<PicturesRepository>().As<IPicturesRepository>();
            builder.RegisterType<PortfolioDbContext>().As<PortfolioDbContext>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
