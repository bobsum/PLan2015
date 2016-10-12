using System;
using System.ComponentModel.DataAnnotations;

namespace Plan2015.Dtos
{
    public class BoxterSwipeDto
    {
        public int Id { get; set; }
        [Required]
        public int SwipeId { get; set; }
        [Required]
        public int ScoutId { get; set; }
        [Required]
        public string ScoutName { get; set; }
        [Required]
        public int HouseId { get; set; }
        [Required]
        public string HouseName { get; set; }
        [Required]
        public string BoxId { get; set; }
        [Required]
        public string BoxIdFriendly { get; set; }
        [Required]
        public string AppMode { get; set; }
        [Required]
        public string AppResponse { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}