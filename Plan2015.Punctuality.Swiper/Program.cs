using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Plan2015.Dtos;

namespace Plan2015.Punctuality.Swiper
{
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAdress"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/punctuality");
                if (!response.IsSuccessStatusCode)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("FEJL!!! Server gav følgende fejl: {0}", response.StatusCode);
                    Console.ResetColor();
                    return;
                }
                
                var punctualities = await response.Content.ReadAsAsync<IEnumerable<PunctualityDto>>();
                var upcomming = punctualities.Where(p => p.Stop > DateTime.Now).OrderBy(p => p.Stop).ToList();
                foreach (var p in upcomming)
                {
                    Console.WriteLine("{0:000}: ({1}) {2}", p.Id, p.Stop, p.Name);
                }

                PunctualityDto punctuality = null;
                while (punctuality == null)
                {
                    Console.Write("#");
                    int punctualityId;

                    if (!int.TryParse(Console.ReadLine(), out punctualityId)) continue;

                    punctuality = upcomming.FirstOrDefault(p => p.Id == punctualityId);
                }
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Svirp tryllestav");
                    var rfid = Console.ReadLine();
                    if (rfid != null && rfid.Equals("q", StringComparison.InvariantCultureIgnoreCase)) break;
                    Console.Clear();
                    if (punctuality.Stop < DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Deadline er overskredet!");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (rfid == null) continue;

                        var swipe = new PunctualitySwipeDto
                        {
                            PunctualityId = punctuality.Id,
                            Rfid = rfid,
                            //Time = DateTime.Now
                        };

                        response = await client.PostAsJsonAsync("api/punctualityswipe", swipe);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Svirp godkendt!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Svirp ikke godkendt prøv igen!");
                            Console.ResetColor();
                        }
                    }
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
