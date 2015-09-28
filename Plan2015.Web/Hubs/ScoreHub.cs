using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Plan2015.Data;

namespace Plan2015.Web.Hubs
{
    public class ScoreHub : Hub<IScoreHubClient>
    {
        public override Task OnConnected()
        {
            var calculator = new ScoreCalculator();
            using (var db = new DataContext())
            {
                Clients.Caller.Updated(calculator.GetScore(db));
                Debug.WriteLine("Hello");
            }
            return base.OnConnected();
        }
    }
}