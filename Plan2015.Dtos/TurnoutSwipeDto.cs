using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class TurnoutSwipeDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Data { get; set; }
    }
}