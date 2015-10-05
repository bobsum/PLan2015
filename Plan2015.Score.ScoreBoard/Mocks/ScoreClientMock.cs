using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Plan2015.Score.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bismuth.Framework.Input;
using Plan2015.Dtos;

namespace Plan2015.Score.ScoreBoard.Mocks
{
    public class ScoreClientMock : IScoreClient
    {
        private readonly List<SchoolScore> _schoolScores = new List<SchoolScore>();

        public ScoreClientMock()
        {

        }

        public void Start()
        {
        }

        public IEnumerable<SchoolScore> SchoolScores
        {
            get { return _schoolScores; }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyPressedOnce(Keys.A))
            {
                AddSchool("Agernholdt", "Rasmus", "Mads", "Peter", "Jergen");
                AddSchool("Hardenberg", "Blablabla", "Mads", "Peter", "Jergen");
                AddSchool("Ravnsborg", "Blumensaat", "Mads", "Peter", "Jergen");
                Initialized();
            }

            if (ks.IsKeyDown(Keys.Q))
            {
                IncrementHouse(ks, _schoolScores[0]);
            }
            else if (ks.IsKeyDown(Keys.W))
            {
                IncrementHouse(ks, _schoolScores[1]);
            }
            else if (ks.IsKeyDown(Keys.E))
            {
                IncrementHouse(ks, _schoolScores[2]);
            }
            else
            {
                if (ks.IsKeyPressedOnce(Keys.D1)) _schoolScores[0].Amount++;
                if (ks.IsKeyPressedOnce(Keys.D2)) _schoolScores[1].Amount++;
                if (ks.IsKeyPressedOnce(Keys.D3)) _schoolScores[2].Amount++;
            }
        }

        private void IncrementHouse(KeyboardState ks, SchoolScore school)
        {
            if (ks.IsKeyPressedOnce(Keys.D1)) school.HouseScores.ToArray()[0].Amount++;
            if (ks.IsKeyPressedOnce(Keys.D2)) school.HouseScores.ToArray()[1].Amount++;
            if (ks.IsKeyPressedOnce(Keys.D3)) school.HouseScores.ToArray()[2].Amount++;
            if (ks.IsKeyPressedOnce(Keys.D4)) school.HouseScores.ToArray()[3].Amount++;
        }

        public Action Initialized { get; set; }

        private void AddSchool(string schoolName, params string[] houseNames)
        {
            SchoolScore school = new SchoolScore { Name = schoolName };
            school.Amount = 2343;
            for (int i = 0; i < houseNames.Length; i++)
            {
                AddHouse(school, houseNames[i], i);
            }

            _schoolScores.Add(school);
        }

        private void AddHouse(SchoolScore school, string houseName, int id)
        {
            school.UpdateHouseScores(new[]
            {
                new HouseScoreDto
                {
                    Id = id,
                    Name = houseName,
                    Amount = 4590
                }
            });
        }
    }
}
