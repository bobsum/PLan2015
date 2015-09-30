using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class ScoutDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}