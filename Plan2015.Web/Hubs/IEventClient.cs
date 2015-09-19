using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IEventClient
    {
        void Add(EventDto @event);
        void Update(EventDto @event);
        void Remove(int id);
    }
}