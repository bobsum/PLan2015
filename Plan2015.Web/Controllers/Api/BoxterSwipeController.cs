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
    public class BoxterSwipeController : ApiControllerWithDB
    {
        public async Task<IEnumerable<BoxterSwipeDto>> GetSwipes()
        {
            return await Db.BoxterSwipes.Select(ToDto()).ToListAsync();
        }

        private static Expression<Func<BoxterSwipe, BoxterSwipeDto>> ToDto()
        {
            return b => new BoxterSwipeDto
            {
                Id = b.Id,
                SwipeId = b.SwipeId,
                ScoutId = b.ScoutId,
                ScoutName = b.Scout.Name,
                HouseId = b.Scout.HouseId,
                HouseName = b.Scout.House.Name,
                AppMode = b.AppMode,
                AppResponse = b.AppResponse,
                BoxId = b.BoxId,
                BoxIdFriendly = b.BoxIdFriendly,
                CreateDate = b.CreateDate
            };
        }
    }
}