using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Plan2015.Score.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://localhost:49383/

            var connection = new HubConnection("http://localhost:49383/");

            var hub = connection.CreateHubProxy("EventHub");

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }
            }).Wait();

            hub.On<object>("update", Console.WriteLine);

            Console.ReadLine();
        }
    }
}
