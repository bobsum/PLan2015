using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Plan2015.Data;

namespace Plan2015.Sorting.Hat
{
    class Program
    {
        private const string LINE_FORMAT = @"^\x02(\w{12})\x03\x03$"; //todo ret

        private static void Main(string[] args)
        {
            while (true)
            {
                using (var db = new DataContext())
                {
                    var line = Console.ReadLine();
                    Console.Clear();

                    if (line == null) continue;
                    var match = Regex.Match(line, LINE_FORMAT);
                    if (!match.Success) continue;

                    var rfid = match.Groups[1].ToString();

                    var scout = db.Scouts
                        .Include(s => s.House)
                        .FirstOrDefault(s => s.Rfid == rfid);

                    if (scout == null) continue;

                    Console.WriteLine("Navn: {0}", scout.Name);
                    Console.Write("Kollegie: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(scout.House.Name);
                    Console.ResetColor();

                    if (string.IsNullOrEmpty(scout.Info)) continue;

                    Console.WriteLine();
                    Console.WriteLine("Ekstra info: {0}", scout.Info);
                }
            }
        }
    }
}
