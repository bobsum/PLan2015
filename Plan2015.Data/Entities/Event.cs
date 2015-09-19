using System;
using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class Punctuality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public bool All { get; set; }
        //public int Amount { get; set; }
    }

    public class PunctualitySwipe
    {
        public int Id { get; set; }
        public int PunctualityId { get; set; }
        public virtual Punctuality Punctuality { get; set; }
        public int ScoutId { get; set; }
        public virtual Scout Scout { get; set; }
        public DateTime Time { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public virtual List<EventPoint> Points { get; set; }
    }
}
