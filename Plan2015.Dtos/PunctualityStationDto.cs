using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityStationDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool DefaultAll { get; set; }
    }
}