using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public virtual List<EventPoint> Points { get; set; }
    }
}
