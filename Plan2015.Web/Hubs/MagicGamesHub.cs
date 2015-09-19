using Microsoft.AspNet.SignalR;
using Plan2015.Web.Models;

namespace Plan2015.Web.Hubs
{
    public class MagicGamesHub : Hub<IMagicGamesClient>
    {
    }

    public interface IMagicGamesClient
    {
        void Update(MagicGamesHouseDto house);
    }
}