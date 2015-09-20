using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class MagicGamesScoreController : ApiControllerWithDB
    {
        public async Task<IEnumerable<MagicGamesScoreDto>> GetScores()
        {
            return await Db.Houses.Select(ToDto()).ToListAsync();
        }

        private Expression<Func<House, MagicGamesScoreDto>> ToDto()
        {
            return h => new MagicGamesScoreDto
            {
                Id = h.Id,
                Name = h.Name,
                MarkerPoints = h.MagicGamesMarkerPoints.Count,
                TimePoints = h.MagicGamesTimePoints.Count
            };
        }
    }
}