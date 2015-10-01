using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityStatusHouseDto : HouseDto
    {
        [Required]
        public IEnumerable<PunctualityStatusScoutDto> Scouts { get; set; }
    }
}