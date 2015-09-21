using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface ILessonClient
    {
        void Add(LessonDto lesson);
        void Update(LessonDto lesson);
        void Remove(int id);
    }
}