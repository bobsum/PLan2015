namespace Plan2015.Data.Entities
{
    public class ActivityPoint
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}