using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;
using Plan2015.Dtos;

namespace Plan2015.Score.Client
{
    public class ScoreClient : IScoreClient
    {
        private readonly IDictionary<int, SchoolScore> _schoolScores = new Dictionary<int, SchoolScore>();

        public ScoreClient()
        {
            var connection = new HubConnection("http://localhost:2015/");

            var hub = connection.CreateHubProxy("ScoreHub");

            hub.On<IEnumerable<SchoolScoreDto>>("Updated", UpdateSchoolScores);

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
            }).Wait();
        }

        private void UpdateSchoolScores(IEnumerable<SchoolScoreDto> schools)
        {
            foreach (var school in schools)
            {
                SchoolScore score;
                if (!_schoolScores.TryGetValue(school.Id, out score))
                {
                    score = new SchoolScore
                    {
                        Id = school.Id,
                        Name = school.Name
                    };
                    _schoolScores.Add(school.Id, score);
                }
                score.UpdateHouseScores(school.Houses);
            }

            Console.Clear();
            foreach (var schoolScore in SchoolScores)
            {
                Console.WriteLine("{0} : {1}", schoolScore.Name, schoolScore.Amount);
                foreach (var houseScore in schoolScore.HouseScores)
                {
                    Console.WriteLine("{0} : {1}", houseScore.Name, houseScore.Amount);
                }
            }
        }

        public IEnumerable<SchoolScore> SchoolScores
        {
            get { return _schoolScores.Values; }
        }
    }
}
