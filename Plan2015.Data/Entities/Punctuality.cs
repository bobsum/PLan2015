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
        public virtual List<PunctualitySwipe> Swipes { get; set; } 
    }
}