using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface ITurnoutPointClient
    {
        void Add(TurnoutPointDto turnoutPoint);
    }
}