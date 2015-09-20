using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Scout> Scouts { get; set; }
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        public virtual List<MagicGamesMarkerPoint> MagicGamesMarkerPoints { get; set; }
        public virtual List<MagicGamesTimePoint> MagicGamesTimePoints { get; set; }
    }
}