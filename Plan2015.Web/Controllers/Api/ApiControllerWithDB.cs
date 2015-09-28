using System;
using System.Web.Http;
using Plan2015.Data;
using Plan2015.Web.Filters;
using Microsoft.AspNet.SignalR;
using Plan2015.Web.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    [InvalidModelStateFilter]
    public abstract class ApiControllerWithDB : ApiController
    {
        private readonly Lazy<DataContext> _db = new Lazy<DataContext>(
            () => new DataContext()
            );

        private readonly Lazy<IHubContext> _hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<ScoreHub>()
        );

        protected DataContext Db
        {
            get { return _db.Value; }
        }

        protected IHubContext ScoreHub
        {
            get { return _hub.Value; }
        }
    }
}