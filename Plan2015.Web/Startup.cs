using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Plan2015.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace Plan2015.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration
                .Configuration
                .Formatters
                .JsonFormatter
                .SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();

            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new SignalRContractResolver()
            });
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            app.MapSignalR();
        }

        public class SignalRContractResolver : IContractResolver
        {
            private readonly Assembly _assembly;
            private readonly IContractResolver _camelCaseContractResolver;
            private readonly IContractResolver _defaultContractSerializer;

            public SignalRContractResolver()
            {
                _defaultContractSerializer = new DefaultContractResolver();
                _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
                _assembly = typeof(Connection).Assembly;
            }

            #region IContractResolver Members

            public JsonContract ResolveContract(Type type)
            {
                if (type.Assembly.Equals(_assembly))
                    return _defaultContractSerializer.ResolveContract(type);

                return _camelCaseContractResolver.ResolveContract(type);
            }

            #endregion
        }
    }
}
