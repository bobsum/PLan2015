using System;
using System.Collections.Generic;

namespace Plan2015.Score.Client
{
    public interface IScoreClient
    {
        void Start();
        Action Initialized { get; set; }
        IEnumerable<SchoolScore> SchoolScores { get; }
    }
}