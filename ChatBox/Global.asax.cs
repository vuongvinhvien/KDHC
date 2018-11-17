using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Store.Data.DataDbContext;
using Store.Data.Repositories;

using Store.Sevices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChatBox
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // ChatBox.App_Start.ChatIRStart.Configuration();
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DataChatBox>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AccountResponsitory>().As<IAccountResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();
            builder.RegisterType<AccountSevices>().As<IAccountSevices>().InstancePerLifetimeScope();
            builder.RegisterType<SendMailSevices>().As<IIdentityMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreProduce>().As<IStoreProduce>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerResponsitory>().As<ICustomerResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerSevices>().As<ICustomerSevices>().InstancePerLifetimeScope();
            builder.RegisterType<SettingResponsitory>().As<ISettingResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<SettingSevice>().As<ISettingSevice>().InstancePerLifetimeScope();
            builder.RegisterType<ChatLineSevices>().As<IChatLineSevices>().InstancePerLifetimeScope();
            builder.RegisterType<VisitorSevices>().As<IVisitorSevices>().InstancePerLifetimeScope();
            builder.RegisterType<VisitorResponsitory>().As<IVisitorResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<ChatLineResponsitory>().As<IChatLineResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<RolesResponsitory>().As<IRolesResponsitory>().InstancePerLifetimeScope();
            builder.RegisterType<RolesServices>().As<IRolesServices>().InstancePerLifetimeScope();
            builder.RegisterType<AspNetRoleResponsitory>().As<IAspNetRoleResponsitory>().InstancePerLifetimeScope();
          
            builder.Register(
         c => new MapperConfiguration(cfg =>
         {
             cfg.AddProfile(new ChatBox.Mapping.Mapping());
             
         }))
         .AsSelf()
         .SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
             .As<IMapper>()
             .InstancePerLifetimeScope();
            IContainer container = builder.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
