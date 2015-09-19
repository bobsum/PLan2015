using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Web.Models
{
    public class MagicGamesHouseDto
    {
        [Required]
        public int HouseId { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public IEnumerable<MagicGamesIntervalDto> Intervals { get; set; }
    }
}