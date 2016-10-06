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

            dto = await Db.Activities.Select(ToDto()).SingleAsync(a => a.Id == entity.Id);

            Hub.Clients.All.Add(dto);

            return CreatedAtRoute("DefaultApi", new {id = dto.Id}, dto);
        }

        public async Task<ActivityDto> PutActivity(ActivityDto dto)
        {
            foreach (var point in dto.Points)
            {
                var ep = Db.ActivityPoints.Single(p => p.Id == point.Id);
                ep.Amount = point.Amount;
                ep.Visible = point.Visible;
            }

            await Db.SaveChangesAsync();

            dto = await Db.Activities.Select(ToDto()).SingleAsync(a => a.Id == dto.Id);
            
            Hub.Clients.All.Update(dto);
            ScoreHub.Clients.All.Updated(Repository.GetScore(Db));
            return dto;
        }

        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            var entity = await Db.Activities.SingleAsync(a => a.Id == id);
            Db.Activities.Remove(entity);

            await Db.SaveChangesAsync();
            
            Hub.Clients.All.Remove(id);
            ScoreHub.Clients.All.Updated(Repository.GetScore(Db));
            return Ok();
        }

        private Expression<Func<Activity, ActivityDto>> ToDto()
        {
            return a => new ActivityDto
            {
                Id = a.Id,
                Name = a.Name,
                TotalPoints = a.TotalPoints,
                Points = a.Points.Select(p => new ActivityPointDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    HouseId = p.House.Id,
                    HouseName = p.House.Name,
                    Visible = p.Visible
                })
            };
        }
    }
}