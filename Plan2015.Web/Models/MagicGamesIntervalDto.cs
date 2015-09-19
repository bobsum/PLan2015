using System.ComponentModel.DataAnnotations;

namespace Plan2015.Web.Models
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