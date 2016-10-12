using System;

namespace Plan2015.Data.Entities
{
    public class BoxterSwipe
    {
        public int Id { get; set; }
        public int SwipeId { get; set; }
        public int ScoutId { get; set; }
        public virtual Scout Scout { get; set; }
        public string BoxId { get; set; }
        public string BoxIdFriendly { get; set; }
        public string AppMode { get; set; }
        public string AppResponse { get; set; }
        public DateTime CreateDate { get; set; }
    }
}