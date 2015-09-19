using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;

namespace Plan2015.Score.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://localhost:49383/

            var connection = new HubConnection("http://localhost:8080/");

            var hub = connection.CreateHubProxy("ScoreHub");

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

            hub.On<int,int>("ScoreChanged", (houseId, score) => Console.WriteLine("{0}: {1}", houseId, score));

            Console.ReadLine();
        }
    }
}
