using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityStatusDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool All { get; set; }
        [Required]
        public IEnumerable<PunctualityStatusHouseDto> Houses { get; set; }
    }
}