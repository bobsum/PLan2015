using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.MagicGames.TimeSwiper
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var start = DateTime.Now;
            using (var db = new DataContext())
            {
                foreach (var squad in db.Squads)
                {
                    squad.HungerGamesPoints = 0;
                }
                foreach (var scout in db.Scouts)
                {
                    scout.HungerScan = start;
                }
                db.SaveChanges();
            }

            while (true)
            {
                using (var db = new DataContext())
                {
                    var line = Console.ReadLine();

                    var scout = db.GetScoutByScan(line);

                    var now = DateTime.Now;

                    if (scout == null ||
                        !scout.HungerInterval.HasValue ||
                        ((now - start).Minutes % scout.HungerInterval.Value) != 0 ||
                        !scout.HungerScan.HasValue ||
                        (now - scout.HungerScan.Value).Minutes == 0)
                        continue;

                    scout.Squad.HungerGamesPoints++;
                    scout.HungerScan = now;

                    db.SaveChanges();
                }
            }*/
        }
    }
}
