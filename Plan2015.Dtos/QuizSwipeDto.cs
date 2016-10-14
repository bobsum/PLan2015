using System;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class QuizSwipeDto
    {
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public long Rfid { get; set; }
    }
}