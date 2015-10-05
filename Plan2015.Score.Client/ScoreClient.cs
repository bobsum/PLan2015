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
        private readonly HubConnection _connection;
        private bool _isInitialized;

        public ScoreClient(string url = "http://localhost:2015/")
        {
            _connection = new HubConnection(url);

            var hub = _connection.CreateHubProxy("ScoreHub");

            hub.On<IEnumerable<SchoolScoreDto>>("Updated", UpdateSchoolScores);
        }

        public void Start()
        {
            _connection.Start().ContinueWith(task =>
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
                    score = new SchoolScore { Id = school.Id };
                    _schoolScores.Add(school.Id, score);
                }
                score.Name = school.Name;
                score.UpdateHouseScores(school.Houses);
            }

            if (!_isInitialized)
            {
                if (Initialized != null) Initialized();
                _isInitialized = true;
            }
        }

        public IEnumerable<SchoolScore> SchoolScores
        {
            get { return _schoolScores.Values; }
        }

        public Action Initialized { get; set; }
    }
}
