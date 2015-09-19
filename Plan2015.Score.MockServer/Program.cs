using System;
using System.Reflection;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Plan2015.Score.MockServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";
            var random = new Random();
            using (WebApp.Start(url))
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<ScoreHub>();
                Console.WriteLine("MockServer running on {0}", url);
                while (true)
                {
                    Thread.Sleep(random.Next(5000));
                    hub.Clients.All.ScoreChanged(random.Next(1, 13), random.Next(2000));
                }
                Console.ReadLine();
            }
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new SignalRContractResolver()
            });
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class ScoreHub : Hub<IScoreHubClient>
    {
        
    }

    public interface IScoreHubClient
    {
        void ScoreChanged(int houseId, int score);
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
