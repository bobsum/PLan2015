using System;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        [Required]
        public bool All { get; set; }
        [Required]
        public int StationId { get; set; }
        public string StationName { get; set; }
    }
}