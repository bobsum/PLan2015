using System;
using System.Data.Entity;
using System.Linq;
using Plan2015.Data;

namespace Plan2015.Sorting.Hat
{
    class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                using (var db = new DataContext())
                {
                    var rfid = Console.ReadLine();
                    Console.Clear();
                    if (rfid == null) continue;
                    
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
