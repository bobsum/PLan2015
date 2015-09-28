using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using Plan2015.Dtos;

namespace Plan2015.Score.ConsoleClient
{
    class ScoreClient
    {
        public ScoreClient()
        {
            var connection = new HubConnection("http://localhost:2015/");

            var hub = connection.CreateHubProxy("ScoreHub");

            hub.On<IEnumerable<SchoolScoreDto>>("Updated", schools =>
            {
                _schools = schools;
                Console.Clear();
                foreach (var school in _schools)
                {
                    Console.WriteLine(school.Name);
                    foreach (var house in school.Houses)
                    {
                        Console.WriteLine("  {0} : {1}", house.Name, house.Amount);
                    }
                }
            });

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
        }

        private IEnumerable<SchoolScoreDto> _schools;
    }
}