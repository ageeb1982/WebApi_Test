using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace WebApi_Test
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();


            // لتحديد شكل المخرجات نضيف في الرابط كلمة 
            //type=json or type=xml 
            config.Formatters.JsonFormatter
                .MediaTypeMappings
                .Add(new QueryStringMapping("type", "json", "application/json"));

            config.Formatters.XmlFormatter
               .MediaTypeMappings
               .Add(new QueryStringMapping("type", "xml", "application/xml"));


            //لتنسيق النتائج مرتبة
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
