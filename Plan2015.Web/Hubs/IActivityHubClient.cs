using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IActivityHubClient
    {
        void Add(ActivityDto activity);
        void Update(ActivityDto activity);
        void Remove(int id);
    }
}