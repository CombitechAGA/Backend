using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AgaBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.Error =
            //    delegate(object sender, ErrorEventArgs args)
            //    {
            //        System.Diagnostics.EventLog.WriteEntry(args.ErrorContext.Error.TargetSite.DeclaringType.ToString(), args.ErrorContext.Error.Message, EventLogEntryType.Error);
            //    };
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
            //jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            //jsonFormatter.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
        }
    }
}
