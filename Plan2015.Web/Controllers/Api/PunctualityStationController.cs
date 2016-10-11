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
    public class PunctualityStationController : ApiControllerWithDB
    {
        public async Task<IEnumerable<PunctualityStationDto>> GetStations()
        {
            return await Db.PunctualityStations.Select(ToDto()).ToListAsync();
        }

        private Expression<Func<PunctualityStation, PunctualityStationDto>> ToDto()
        {
            return p => new PunctualityStationDto
            {
                Id = p.Id,
                Name = p.Name,
                DefaultAll = p.DefaultAll
            };
        }
    }
}