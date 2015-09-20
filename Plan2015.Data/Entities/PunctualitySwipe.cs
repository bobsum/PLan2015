using System;

namespace Plan2015.Data.Entities
{
    public class PunctualitySwipe
    {
        public int Id { get; set; }
        public int PunctualityId { get; set; }
        public virtual Punctuality Punctuality { get; set; }
        public int ScoutId { get; set; }
        public virtual Scout Scout { get; set; }
        public DateTime Time { get; set; }
    }
}