using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IMagicGamesSetupHubClient
    {
        void Update(MagicGamesSetupDto setup);
    }
}