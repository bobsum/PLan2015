using System;

namespace Plan2015.Data.Entities
{
    public class MagicGamesTimePoint
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public DateTime Time { get; set; }
        public virtual House House { get; set; }
    }
}