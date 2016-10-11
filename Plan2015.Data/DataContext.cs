using System.Data.Entity;
using System.Diagnostics;
using Plan2015.Data.Entities;

namespace Plan2015.Data
{
    //Import-Module .\packages\EntityFramework.6.1.1\tools\EntityFramework.psm1
    public class DataContext : DbContext
    {
        public DataContext() : base("Plan2016")
        {
            //Configuration.ProxyCreationEnabled = false;
            //Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<School> Schools { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityPoint> ActivityPoints { get; set; }

        public DbSet<QuizPoint> QuizPoints { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        
        public DbSet<TurnoutPoint> TurnoutPoints { get; set; }
        
        public DbSet<Punctuality> Punctualities { get; set; }
        public DbSet<PunctualityPoint> PunctualityPoints { get; set; }
        public DbSet<PunctualityStation> PunctualityStations { get; set; }
        public DbSet<PunctualitySwipe> PunctualitySwipes { get; set; }

        public DbSet<MagicGamesMarkerPoint> MagicGamesMarkerPoints { get; set; }
        public DbSet<MagicGamesTimePoint> MagicGamesTimePoints { get; set; }
    }
}