using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class PunctualityStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool DefaultAll { get; set; }
        public virtual List<Punctuality> Punctualities { get; set; }
    }
}