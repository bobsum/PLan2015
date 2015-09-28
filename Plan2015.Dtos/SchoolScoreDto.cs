using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class SchoolScoreDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IEnumerable<HouseScoreDto> Houses { get; set; }
    }
}