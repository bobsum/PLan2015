using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plan2015.Data.Entities
{
    public class MagicGamesInterval
    {
        [Key, ForeignKey("Scout")]
        public int ScoutId { get; set; }
        public int Amount { get; set; }
        public virtual Scout Scout { get; set; }
    }
}