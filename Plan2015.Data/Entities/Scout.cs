using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class Scout
    {
        public int Id { get; set; }
        public string Rfid { get; set; }
        public string Name { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
        public string Info { get; set; }
        public bool Home { get; set; }
        public virtual MagicGamesInterval MagicGamesInterval { get; set; }
    }
}