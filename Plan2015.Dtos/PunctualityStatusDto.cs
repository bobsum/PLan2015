using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityStatusDto
    {
        public int HouseId { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public IEnumerable<ScoutDto> Arrived { get; set; }
        [Required]
        public IEnumerable<ScoutDto> Missing { get; set; }
    }
}