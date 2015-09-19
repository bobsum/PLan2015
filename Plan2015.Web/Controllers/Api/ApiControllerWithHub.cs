using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    public abstract class ApiControllerWithHub<THub> : ApiControllerWithDB
        where THub : IHub
    {
        private readonly Lazy<IHubContext> _hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
        );

        protected IHubContext Hub
        {
            get { return _hub.Value; }
        }
    }
}