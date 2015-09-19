using System.Collections.Generic;

namespace Plan2015.Data.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<House> Houses { get; set; }
    }
}