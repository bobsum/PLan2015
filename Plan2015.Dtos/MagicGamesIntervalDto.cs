using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class MagicGamesIntervalDto
    {
        [Required]
        public int ScoutId { get; set; }
        [Required]
        public string ScoutName { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}