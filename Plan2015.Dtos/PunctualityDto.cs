using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualityDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public bool All { get; set; }
    }
}