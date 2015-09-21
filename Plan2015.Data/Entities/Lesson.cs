using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public virtual List<ActivityPoint> Points { get; set; }
    }
}
