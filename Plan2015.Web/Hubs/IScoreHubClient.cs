using System.Collections.Generic;
using Plan2015.Dtos;

namespace Plan2015.Web.Hubs
{
    public interface IScoreHubClient
    {
        void Updated(IEnumerable<SchoolScoreDto> score);
    }
}