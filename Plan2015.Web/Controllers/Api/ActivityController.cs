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
    public class ActivityController : ApiControllerWithHub<ActivityHub, IActivityClient>
    {
        /*[ResponseType(typeof (ActivityDto))]
        public async Task<IHttpActionResult> GetActivity(int id)
        {
            var dto = await Db.Activities.Select(ToDto()).SingleOrDefaultAsync(l => l.Id == id);

            if (dto == null) return NotFound();

            return Ok(dto);
        }*/

        public async Task<IEnumerable<ActivityDto>> GetActivities()
        {
            return await Db.Activities.Select(ToDto()).ToListAsync();
        }

        public async Task<IHttpActionResult> PostActivity(ActivityDto dto)
        {
            var entity = new Activity
            {
                Id = dto.Id,
                Name = dto.Name,
                TotalPoints = dto.TotalPoints,
                Points = dto.Points.Select(p => new ActivityPoint{ HouseId = p.HouseId}).ToList()
            };

            Db.Activities.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.Activities.Select(ToDto()).SingleAsync(l => l.Id == entity.Id);

            Hub.Clients.All.Add(dto);

            return CreatedAtRoute("DefaultApi", new {id = dto.Id}, dto);
        }

        public async Task<ActivityDto> PutActivity(ActivityDto dto)
        {
            foreach (var point in dto.Points)
            {
                var ep = Db.ActivityPoints.Single(l => l.Id == point.Id);
                ep.Amount = point.Amount;
            }

            await Db.SaveChangesAsync();

            dto = await Db.Activities.Select(ToDto()).SingleAsync(l => l.Id == dto.Id);
            
            Hub.Clients.All.Update(dto);
            ScoreHub.Clients.All.Updated(Calculator.GetScore(Db));
            return dto;
        }

        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            var entity = await Db.Activities.SingleAsync(l => l.Id == id);
            Db.Activities.Remove(entity);

            await Db.SaveChangesAsync();
            
            Hub.Clients.All.Remove(id);
            ScoreHub.Clients.All.Updated(Calculator.GetScore(Db));
            return Ok();
        }

        private Expression<Func<Activity, ActivityDto>> ToDto()
        {
            return l => new ActivityDto
            {
                Id = l.Id,
                Name = l.Name,
                TotalPoints = l.TotalPoints,
                Points = l.Points.Select(p => new ActivityPointDto
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