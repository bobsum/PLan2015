using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plan2015.Data.Entities;
using Plan2015.Dtos;
using Plan2015.Web.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    public class MagicGamesSetupController : ApiControllerWithHub<MagicGamesSetupHub, IMagicGamesSetupHubClient>
    {
        public async Task<IEnumerable<MagicGamesSetupDto>> GetMagicGames()
        {
            return await Db.Houses.Include(h => h.Scouts).Select(ToDto()).ToListAsync();
        }

        public async Task<MagicGamesSetupDto> PutMagicGamesSetup(MagicGamesSetupDto dto)
        {
            foreach (var interval in dto.Intervals)
            {
                var scout = Db.Scouts.Single(s => s.Id == interval.ScoutId);
                if (scout.MagicGamesInterval == null)
                    scout.MagicGamesInterval = new MagicGamesInterval();

                scout.MagicGamesInterval.Amount = interval.Amount;
            }

            await Db.SaveChangesAsync();

            dto = await Db.Houses.Select(ToDto()).SingleAsync(h => h.HouseId == dto.HouseId);

            Hub.Clients.All.Update(dto);

            return dto;
        }

        private Expression<Func<House, MagicGamesSetupDto>> ToDto()
        {
            return s => new MagicGamesSetupDto
            {
                HouseId = s.Id,
                HouseName = s.Name,
                Intervals = s.Scouts.Where(si => !si.Home).Select(si => new MagicGamesIntervalDto
                {
                    ScoutId = si.Id,
                    ScoutName = si.Name,
                    Amount = si.MagicGamesInterval != null ? si.MagicGamesInterval.Amount : 0
                })
            };
        }
    }
}