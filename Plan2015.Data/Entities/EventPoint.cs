namespace Plan2015.Data.Entities
{
    public class EventPoint
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}