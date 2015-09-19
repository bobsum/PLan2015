using System;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class PunctualitySwipeDto
    {
        public int Id { get; set; }
        [Required]
        public int PunctualityId { get; set; }
        [Required]
        public string Rfid { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}