using GherkinWebAPI.Filter;
using GherkinWebAPI.ValidateModel;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GherkinWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.EnableCors();

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE"); // Enabled for all origin type. 
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Filters.Add(new CheckModelForNullAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ValidateModelStateAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new GherkinwebapiAuth());

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                                .Add(new RequestHeaderMapping("Accept", "text/html",
                                StringComparison.InvariantCultureIgnoreCase,
                                true, "application/json"));

        }
    }
}
