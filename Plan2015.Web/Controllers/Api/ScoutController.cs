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
    public class ScoutController : ApiControllerWithDB
    {
        public async Task<IEnumerable<ScoutDto>> GetScouts()
        {
            return await Db.Scouts.Select(ToDto()).ToListAsync();
        }

        private static Expression<Func<Scout, ScoutDto>> ToDto()
        {
            return s => new ScoutDto
            {
                Id = s.Id,
                Rfid = s.Rfid,
                Name = s.Name,
                HouseName = s.House.Name,
                SchoolName = s.House.School.Name,
                Info = s.Info
            };
        }
    }
}