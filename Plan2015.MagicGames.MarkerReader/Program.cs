using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Plan2015.Data;
using Plan2015.Data.Entities;

namespace Plan2015.MagicGames.MarkerReader
{
    class Program
    {
        private const string LINE_FORMAT = @"^\x02(\w{12})\x03\x03$";

        static void Main()
        {
            var files = Directory.GetFiles("Markers");
            using (var db = new DataContext())
            {
                foreach (var file in files)
                {
                    ReadFile(file, db);
                }
            }
        }

        private static void ReadFile(string file, DataContext db)
        {
            Console.WriteLine("--- {0} ---", file);
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                var markerName = Path.GetFileNameWithoutExtension(file);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var match = Regex.Match(line, LINE_FORMAT);
                    if (!match.Success)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("FEJL!!! Der var en fejl i linjens format: {0}", line);
                        Console.ResetColor();
                        continue;
                    }

                    var rfid = match.Groups[1].ToString();

                    var scout = db.Scouts.FirstOrDefault(s => s.Rfid == rfid);

                    if (scout == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("ADVARSEL!!! Spejder blev ikke fundet");
                        Console.ResetColor();
                        continue;
                    }

                    if (db.MagicGamesMarkerPoints.Any(mp => mp.MarkerName == markerName && mp.HouseId == scout.HouseId))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("ADVARSEL!!! Double svirp");
                        Console.ResetColor();
                        continue;
                    }

                    var point = new MagicGamesMarkerPoint
                    {
                        House = scout.House,
                        MarkerName = markerName
                    };
                    Console.WriteLine("{0} har fået point", scout.House.Name);
                    db.MagicGamesMarkerPoints.Add(point);
                    db.SaveChanges();
                }
            }
        }
    }
}
