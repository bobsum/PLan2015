using System.Data.Entity;
using System.Diagnostics;
using Plan2015.Data.Entities;

namespace Plan2015.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Plan2015")
        {
            //Configuration.ProxyCreationEnabled = false;
            Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventPoint> EventPoints { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<TurnoutPoint> TurnoutPoints { get; set; }
        public DbSet<MarkerPoint> MarkerPoints { get; set; }
        public DbSet<Punctuality> Punctualities { get; set; }
        public DbSet<PunctualitySwipe> PunctualitySwipes { get; set; }
    }
}