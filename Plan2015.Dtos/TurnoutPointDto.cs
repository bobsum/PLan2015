using System;

namespace Plan2015.Dtos
{
    public class TurnoutPointDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int HouseId { get; set; }
        public DateTime Time { get; set; }
        public string HouseName { get; set; }
    }
}