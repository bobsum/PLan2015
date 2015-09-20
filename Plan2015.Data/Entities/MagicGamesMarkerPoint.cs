namespace Plan2015.Data.Entities
{
    public class MagicGamesMarkerPoint
    {
        public int Id { get; set; }
        public string MarkerName { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}