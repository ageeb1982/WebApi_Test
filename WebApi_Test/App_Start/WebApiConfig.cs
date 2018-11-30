using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace WebApi_Test
{


    public class CustomJSONFormatX:JsonMediaTypeFormatter
    {
        public CustomJSONFormatX()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

        }
        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }

    public static class WebApiConfig
    {



        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //------------------------------------------------------------------------------

            // Web API routes
            config.MapHttpAttributeRoutes();

            //------------------------------------------------------------------------------

            // لتحديد شكل المخرجات نضيف في الرابط كلمة 
            //type=json or type=xml 
            config.Formatters.JsonFormatter
                .MediaTypeMappings
                .Add(new QueryStringMapping("type", "json", "application/json"));



            config.Formatters.XmlFormatter
               .MediaTypeMappings
               .Add(new QueryStringMapping("type", "xml", "application/xml"));

            //------------------------------------------------------------------------------


            //لاستخدام الأوامر 
            // public class Cust:IXmlSerializable
            //public void WriteXml 
            //public void ReadXml
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            //------------------------------------------------------------------------------


            //لتنسيق النتائج مرتبة
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //------------------------------------------------------------------------------

            //تحويل الاسماء الكابيتال إلى اسمول في نتائج الجايسون
            // config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            //------------------------------------------------------------------------------

            //لجعل النتائج تأتي على صيغة 
            //Json بصورة دائمة
            //نقوم بحذف xml
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //------------------------------------------------------------------------------

            //لجعل النتائج تأتي على صيغة 
            //xml بصورة دائمة
            //نقوم بحذف Json
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //------------------------------------------------------------------------------



            //الطريقة الأولى [1]-0
            //لجعل النتائج في المتصفح تأتي بصورة افتراضية 
            //json
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));


            //الطريقة الأولى [2]-0
            //لجعل النتائج في المتصفح تأتي بصورة افتراضية 
            //json
            //config.Formatters.Add(new CustomJSONFormatX());




            //------------------------------------------------------------------------------

            //لجعله يقوم بعمل سيريلايز 
            //serialize 
            //ويتجاهل النتائج المتعلقة بالتكرار والتداخل 
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api2/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );


        }
    }
}
