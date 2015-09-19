using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plan2015.Data.Entities;
using Plan2015.Web.Hubs;
using Plan2015.Web.Models;

namespace Plan2015.Web.Controllers.Api
{
    public class HouseController : ApiControllerWithDB
    { 
        public async Task<IEnumerable<HouseDto>> GetHouses()
        {
            return await Db.Houses.Select(ToDto()).ToListAsync();
        }

        private static Expression<Func<House, HouseDto>> ToDto()
        {
            return h => new HouseDto
            {
                Id = h.Id,
                Name = h.Name
            };
        }
    }
}