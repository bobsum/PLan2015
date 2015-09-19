using System.ComponentModel.DataAnnotations;

namespace Plan2015.Web.Models
{
    public class EventPointDto
    {
        public int Id { get; set; }
        [Required]
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}