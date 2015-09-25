using System;
using System.Data.Entity;
using System.Linq;
using Plan2015.Data;
using Plan2015.Data.Entities;

namespace Plan2015.MagicGames.TimeSwiper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--=== Press enter to start ===--");
            Console.ReadLine();

            var start = DateTime.Now;
            
            while (true)
            {
                using (var db = new DataContext())
                {
                    Console.WriteLine("Svirp tryllestav");
                    var rfid = Console.ReadLine();
                    var now = DateTime.Now;
                    Console.Clear();
                    if (start.AddMinutes(61) < now)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Tiden er udløbet!");
                        Console.ResetColor();
                        break;
                    }

                    if (rfid == null) continue;
                    
                    var scout = db.Scouts
                        .Include(s => s.MagicGamesInterval)
                        .FirstOrDefault(s => s.Rfid == rfid);

                    if (scout == null || scout.MagicGamesInterval == null) continue;
                    var lastSwipe = scout.MagicGamesInterval.LastSwipe;
                    if ((lastSwipe.HasValue && (now - lastSwipe.Value).Minutes == 0)) continue;
                    var elapsed = (now - start).Minutes;
                    if (elapsed == 0 || (elapsed % scout.MagicGamesInterval.Amount) != 0) continue;

                    scout.MagicGamesInterval.LastSwipe = now;

                    var point = new MagicGamesTimePoint
                    {
                        House = scout.House,
                        Time = now
                    };
                    db.MagicGamesTimePoints.Add(point);
                    db.SaveChanges();
                }
            }
        }
    }
}
