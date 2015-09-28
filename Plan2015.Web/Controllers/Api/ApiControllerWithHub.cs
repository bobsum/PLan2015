using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    public abstract class ApiControllerWithHub<THub,TClient> : ApiControllerWithDB
        where THub : IHub where TClient : class
    {
        private readonly Lazy<IHubContext<TClient>> _hub = new Lazy<IHubContext<TClient>>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub, TClient>()
        );

        protected IHubContext<TClient> Hub
        {
            get { return _hub.Value; }
        }
    }
}