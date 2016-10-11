using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityStatusDto
    {
        [Required]
        public PunctualityDto Punctuality { get; set; }
        [Required]
        public IEnumerable<PunctualityStatusHouseDto> Houses { get; set; }
    }
}