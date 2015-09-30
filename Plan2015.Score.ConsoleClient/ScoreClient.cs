using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using Plan2015.Dtos;

namespace Plan2015.Score.ConsoleClient
{
    class ScoreClient
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
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
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
                        Id = school.Id, Name = school.Name
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

    public class SchoolScore
    {
        private readonly IDictionary<int, HouseScore> _houseScores = new Dictionary<int, HouseScore>();
        
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<HouseScore> HouseScores
        {
            get { return _houseScores.Values; }
        }

        public int Amount
        {
            get { return HouseScores.Sum(h => h.Amount); }
        }

        public void UpdateHouseScores(IEnumerable<HouseScoreDto> houses)
        {
            foreach (var house in houses)
            {
                HouseScore score;
                if (!_houseScores.TryGetValue(house.Id, out score))
                {
                    score = new HouseScore
                    {
                        Id = house.Id,
                        Name = house.Name
                    };
                    _houseScores.Add(house.Id, score);
                }
                score.Amount = house.Amount;
            }
        }
    }

    public class HouseScore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}