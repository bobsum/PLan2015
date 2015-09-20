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
            }



using (var db = new DataContext())
            {
                foreach (var squad in db.Squads.Where(s=> s.SquadId <= 12).ToList())
                {
                    IList<Scout> scouts = db.Scouts.Where(s => s.SquadId == squad.SquadId).ToList();
                    Console.WriteLine("Patrulje: {0} ({1})", squad.Name, scouts.Count);
                    var valid =  new List<int>(scouts.Count == 7 ? new[]{ 5, 5, 10, 10, 15, 20, 30 }: new[]{ 5, 5, 10, 15, 20, 30 });
                    foreach (var scout in scouts)
                    {
                        Console.WriteLine(string.Join(",", valid));
                        while (!scout.HungerInterval.HasValue)
                        {
                            Console.Write("{0}: ", scout.Name);
                            int interval;
                            if (!int.TryParse(Console.ReadLine(), out interval)) continue;
                            
                            if (valid.Contains(interval))
                                scout.HungerInterval = interval;
                        }
                        valid.Remove(scout.HungerInterval.Value);
                    }
                    Console.WriteLine();
                    foreach (var scout in scouts)
                    {
                        Console.WriteLine("{0}: {1}", scout.Name, scout.HungerInterval);
                    }
                    Console.WriteLine();
                    Console.WriteLine("--- Kontrolere værdier ---");
                    Console.ReadLine();
                    db.SaveChanges();
                    Console.Clear();
                }
            }
            Console.WriteLine("--=== Press enter to start ===--");
            Console.ReadLine();
            var start = DateTime.Now;
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
            }


*/
        }
    }
}
