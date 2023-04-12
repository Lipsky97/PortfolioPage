using Autofac;
using Portfolio.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.DI
{
    public class AFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>().InstancePerRequest();
        }
    }
}