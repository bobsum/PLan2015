using Microsoft.AspNet.SignalR;
using Plan2015.Data;

namespace Plan2015.Web.Hubs
{
    public class PunctualityStatusHub : Hub<IPunctualityStatusHubClient>
    {
        public async void SetId(int id)
        {
            var repository = new Repository();
            using (var db = new DataContext())
            {
                await Groups.Add(Context.ConnectionId, id.ToString());
                Clients.Caller.Updated(repository.GetPunctualityStatus(db, id));
            }
        }
    }
}