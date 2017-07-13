using AutoMapper;
using meditatii.Models;
using meditatii.web.App_Start;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Meditatii.Data;

namespace meditatii
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg => RegisterType(cfg));
        }

        private void RegisterType(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserModel>();
            mapper.CreateMap <Meditatii.Data.Models.User, User>();

            mapper.CreateMap<Category, CategoryModel>();
            mapper.CreateMap<Meditatii.Data.Models.Category, Category>();

            mapper.CreateMap<Cycle, CycleModel>();
            mapper.CreateMap<Meditatii.Data.Models.Cycle, Cycle>();

            mapper.CreateMap<SearchResult<User>, SearchResult<UserModel>>();
        }
    }
}
