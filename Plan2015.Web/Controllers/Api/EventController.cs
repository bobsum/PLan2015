using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Plan2015.Data;
using Plan2015.Data.Entities;
using Plan2015.Web.Hubs;
using Plan2015.Web.Models;

namespace Plan2015.Web.Controllers.Api
{
    public class EventController : ApiControllerWithHub<EventHub>
    {
        [ResponseType(typeof (EventDto))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            var dto = await Db.Events.Select(ToDto()).SingleOrDefaultAsync(e => e.Id == id);

            if (dto == null) return NotFound();

            return Ok(dto);
        }

        public async Task<IEnumerable<EventDto>> GetEvents()
        {
            return await Db.Events.Select(ToDto()).ToListAsync();
        }

        public async Task<IHttpActionResult> PostEvent(EventDto dto)
        {
            var entity = new Event
            {
                Id = dto.Id,
                Name = dto.Name,
                TotalPoints = dto.TotalPoints,
                Points = dto.Points.Select(p => new EventPoint{ HouseId = p.HouseId}).ToList()
            };

            Db.Events.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.Events.Select(ToDto()).SingleAsync(e => e.Id == entity.Id);

            Hub.Clients.All.Add(dto);

            return CreatedAtRoute("DefaultApi", new {id = dto.Id}, dto);
        }

        public async Task<EventDto> PutEvent(EventDto dto)
        {
            foreach (var point in dto.Points)
            {
                var ep = Db.EventPoints.Single(e => e.Id == point.Id);
                ep.Amount = point.Amount;
            }

            await Db.SaveChangesAsync();

            dto = await Db.Events.Select(ToDto()).SingleAsync(e => e.Id == dto.Id);
            
            Hub.Clients.All.Update(dto);

            return dto;
        }

        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            var entity = await Db.Events.SingleAsync(e => e.Id == id);
            Db.Events.Remove(entity);

            await Db.SaveChangesAsync();
            
            Hub.Clients.All.Remove(id);
            
            return Ok();
        }

        private Expression<Func<Event, EventDto>> ToDto()
        {
            return e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                TotalPoints = e.TotalPoints,
                Points = e.Points.Select(p => new EventPointDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    HouseId = p.House.Id,
                    HouseName = p.House.Name
                })
            };
        }
    }
}