using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class ScoutDto
    {
        public int Id { get; set; }
        [Required]
        public string Rfid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Info { get; set; }
    }
}