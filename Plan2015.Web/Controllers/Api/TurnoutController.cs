using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Plan2015.Data.Entities;
using Plan2015.Dtos;
using Plan2015.Web.Hubs;

namespace Plan2015.Web.Controllers.Api
{
    public class TurnoutController : ApiControllerWithHub<TurnoutPointHub, ITurnoutPointHubClient>
    {
        public async Task<IEnumerable<TurnoutPointDto>> GetTurnout()
        {
            return await Db.TurnoutPoints.Select(ToDto()).ToListAsync();
        }

        public async Task<IHttpActionResult> PostTurnout(TurnoutPointDto dto)
        {
            var entity = new TurnoutPoint()
            {
                HouseId = dto.HouseId,
                Amount = dto.Amount,
                Time = DateTime.Now,
                Discarded = false,
            };

            Db.TurnoutPoints.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.TurnoutPoints.Select(ToDto()).SingleAsync(p => p.Id == entity.Id);

            Hub.Clients.All.Add(dto);
            ScoreUpdated();

            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }

        public async Task<IHttpActionResult> DeleteTurnout(int id)
        {
            var entity = await Db.TurnoutPoints.SingleAsync(l => l.Id == id);

            entity.Discarded = true;

            await Db.SaveChangesAsync();

            Hub.Clients.All.Remove(id);
            ScoreUpdated();
            return Ok();
        }

        private Expression<Func<TurnoutPoint, TurnoutPointDto>> ToDto()
        {
            return t => new TurnoutPointDto
            {
                Id = t.Id,
                HouseId = t.HouseId,
                HouseName = t.House.Name,
                Amount = t.Amount,
                Time = t.Time
            };
        }
    }
}