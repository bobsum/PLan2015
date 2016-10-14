using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class TeamMemberDto
    {
        public int Id { get; set; }
        [Required]
        public long Rfid { get; set; }
        [Required]
        public string Name { get; set; }

    }
}