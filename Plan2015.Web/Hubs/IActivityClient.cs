using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IActivityClient
    {
        void Add(ActivityDto activity);
        void Update(ActivityDto activity);
        void Remove(int id);
    }
}