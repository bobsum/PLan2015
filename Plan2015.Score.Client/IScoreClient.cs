using System;
using System.Collections.Generic;

namespace Plan2015.Score.Client
{
    public interface IScoreClient
    {
        Action<SchoolScore> SchoolScoreAdded { get; set; }
        IEnumerable<SchoolScore> SchoolScores { get; }
    }
}