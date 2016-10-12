using System;

namespace Plan2015.Data.Entities
{
    public class TurnoutPoint
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public bool Discarded { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
        public DateTime Time { get; set; }
    }
}