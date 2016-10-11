using Microsoft.AspNet.SignalR;
using Plan2015.Data;

namespace Plan2015.Web.Hubs
{
    public class PunctualityHub : Hub<IPunctualityHubClient>
    {
        public void SetId(int? newId, int? oldId)
        {
            if (oldId.HasValue) Groups.Remove(Context.ConnectionId, oldId.ToString());

            if (!newId.HasValue) return;

            Groups.Add(Context.ConnectionId, newId.ToString());
            var repository = new Repository();

            using (var db = new DataContext())
            {
                Groups.Add(Context.ConnectionId, newId.ToString());
                Clients.Caller.UpdatedStatus(repository.GetStatus(db, newId.Value));
            }
        }
    }
}