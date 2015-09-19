using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    /*public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public bool All { get; set; }
        public int Amount { get; set; }
    }

    public class EventSwipe
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public int ScoutId { get; set; }
        public virtual Scout Scout { get; set; }
        public DateTime Time { get; set; }
    }*/

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public virtual List<EventPoint> Points { get; set; }
    }
}
