using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Plan2015.Data;

namespace Plan2015.Points.BookReader
{
    class Program
    {
        private const string LINE_FORMAT = @"^([+-])\x02(\w{12})\x03\x03$";
        private const int MAX_POINTS = 10 * 6;

        static void Main()
        {
            var files = Directory.GetFiles("Books");
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
                string line;
                TeamMember teamMember = null;
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

                    var sign = match.Groups[1].ToString();
                    var rfid = match.Groups[2].ToString();

                    var member = db.TeamMembers.FirstOrDefault(t => t.Rfid == rfid);
                    if (member != null)
                    {
                        teamMember = member;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Teammedlem ændret til: {0}", teamMember.Name);
                        Console.ResetColor();
                        continue;
                    }

                    if (teamMember != null)
                    {
                        var totalPoints = db.TeamPoints
                            .Where(tp => tp.TeamMemberId == teamMember.Id)
                            .Select(tp => tp.Amount)
                            .ToList()
                            .Sum(a => Math.Abs(a));

                        if (totalPoints >= MAX_POINTS)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("ADVARSEL!!! {0} har brugt alle sine points", teamMember.Name);
                            Console.ResetColor();
                            teamMember = null;
                        }
                    }

                    var scout = db.Scouts.FirstOrDefault(s => s.Rfid == rfid);

                    if (scout == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("ADVARSEL!!! Spejder blev ikke fundet");
                        Console.ResetColor();
                        continue;
                    }

                    if (teamMember == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("ADVARSEL!!! Point blev gem uden teammedlem");
                        Console.ResetColor();
                    }
                    var point = new TeamPoint
                    {
                        Amount = sign == "+" ? 1 : -1,
                        House = scout.House,
                        TeamMember = teamMember
                    };
                    Console.WriteLine("{0}/{1} har fået {2} points", scout.House.Name, scout.Name, point.Amount);
                    db.TeamPoints.Add(point);
                    db.SaveChanges();
                }
            }
        }
    }
}
