using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Punctuality.Swiper
{
    class Program
    {
        static void Main(string[] args)
        {
            //vent på swipe
            //send til Api hvis online eller log til disk

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49383/api/punctuality");
                //client.PostAsJsonAsync()
                //http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            }

        }


    }
}
