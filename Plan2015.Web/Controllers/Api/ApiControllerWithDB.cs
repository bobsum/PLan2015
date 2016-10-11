using System;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Plan2015.Data;
using Plan2015.Web.Filters;
using Plan2015.Web.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    [InvalidModelStateFilter]
    public abstract class ApiControllerWithDB : ApiController
    {
        protected readonly Repository Repository = new Repository();

        private readonly Lazy<DataContext> _db = new Lazy<DataContext>(
            () => new DataContext()
        );

        private readonly Lazy<IHubContext<IScoreHubClient>> _scoreHub = new Lazy<IHubContext<IScoreHubClient>>(
            () => GlobalHost.ConnectionManager.GetHubContext<ScoreHub, IScoreHubClient>()
        );

        protected DataContext Db
        {
            get { return _db.Value; }
        }

        protected void ScoreUpdated()
        {
            _scoreHub.Value.Clients.All.Updated(Repository.GetScore(_db.Value));
        }
    }
}