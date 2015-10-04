using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Plan2015.Score.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Mocks
{
    public class ScoreClientMock : IScoreClient
    {
        private readonly List<SchoolScore> _schoolScores = new List<SchoolScore>();

        public ScoreClientMock()
        {

        }

        public IEnumerable<SchoolScore> SchoolScores
        {
            get { return _schoolScores; }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
        }

        public Action<SchoolScore> SchoolScoreAdded { get; set; }
    }
}
