namespace Plan2015.Data.Entities
{
    public class MarkerPoint
    {
        public int Id { get; set; }
        public string MarkerName { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}