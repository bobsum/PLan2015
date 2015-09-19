namespace Plan2015.Data.Entities
{
    public class TurnoutPoint
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int? TeamMemberId { get; set; }
        public virtual TeamMember TeamMember { get; set; }
        public int HouseId { get; set; }
        public virtual House House { get; set; }
    }
}