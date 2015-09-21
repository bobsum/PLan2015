using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public virtual List<LessonPoint> Points { get; set; }
    }
}
