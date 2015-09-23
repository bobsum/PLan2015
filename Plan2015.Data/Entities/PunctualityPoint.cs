namespace Plan2015.Data.Entities
{
    public class PunctualityPoint
    {
        public int Id { get; set; }
        public int PunctualityId { get; set; }
        public virtual Punctuality Punctuality { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}