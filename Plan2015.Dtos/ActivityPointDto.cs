using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class ActivityPointDto
    {
        public int Id { get; set; }
        [Required]
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        [Required]
        public int Amount { get; set; }
        public bool Visible { get; set; }
    }
}