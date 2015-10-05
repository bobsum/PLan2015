using System.Collections.Generic;
using Plan2015.Dtos;

namespace Plan2015.Score.Client
{
    public class SchoolScore
    {
        private readonly IDictionary<int, HouseScore> _houseScores = new Dictionary<int, HouseScore>();

        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<HouseScore> HouseScores
        {
            get { return _houseScores.Values; }
        }

        public int Amount { get; set; }

        public void UpdateHouseScores(IEnumerable<HouseScoreDto> houses)
        {
            var amount = 0;
            foreach (var house in houses)
            {
                HouseScore score;
                if (!_houseScores.TryGetValue(house.Id, out score))
                {
                    score = new HouseScore { Id = house.Id };
                    _houseScores.Add(house.Id, score);
                }
                score.Name = house.Name;
                score.Amount = house.Amount;
                amount += house.Amount;
            }
            Amount = amount;
        }
    }
}