using System.Collections.Generic;

namespace Plan2015.Score.Client
{
    public interface IScoreClient
    {
        IEnumerable<SchoolScore> SchoolScores { get; }
    }
}