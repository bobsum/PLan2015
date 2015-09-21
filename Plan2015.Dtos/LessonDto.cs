using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TotalPoints { get; set; }
        [Required]
        public IEnumerable<LessonPointDto> Points { get; set; }
    }
}