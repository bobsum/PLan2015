using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class HouseScoreDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int HiddenAmount { get; set; }
    }
}