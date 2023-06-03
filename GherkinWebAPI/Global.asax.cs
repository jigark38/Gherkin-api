using AutoMapper;
using GherkinWebAPI.Automapper;
using GherkinWebAPI.Filter;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GherkinWebAPI
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AutoMapperProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            //GherkinLib.GherkinUtils.RegisterService(context);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GherkinLib.GherkinUtils.GetConfiguration();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            AutoMapperConfiguration.Configure();
        }
    }

}
