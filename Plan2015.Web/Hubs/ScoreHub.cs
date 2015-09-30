using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Plan2015.Data;

namespace Plan2015.Web.Hubs
{
    public class ScoreHub : Hub<IScoreHubClient>
    {
        public override Task OnConnected()
        {
            var repository = new Repository();
            using (var db = new DataContext())
            {
                Clients.Caller.Updated(repository.GetScore(db));
            }
            return base.OnConnected();
        }
    }
}