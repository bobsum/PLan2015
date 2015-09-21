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
    public class LessonController : ApiControllerWithHub<LessonHub>
    {
        /*[ResponseType(typeof (LessonDto))]
        public async Task<IHttpActionResult> GetLesson(int id)
        {
            var dto = await Db.Lessons.Select(ToDto()).SingleOrDefaultAsync(l => l.Id == id);

            if (dto == null) return NotFound();

            return Ok(dto);
        }*/

        public async Task<IEnumerable<LessonDto>> GetLessons()
        {
            return await Db.Lessons.Select(ToDto()).ToListAsync();
        }

        public async Task<IHttpActionResult> PostLesson(LessonDto dto)
        {
            var entity = new Lesson
            {
                Id = dto.Id,
                Name = dto.Name,
                TotalPoints = dto.TotalPoints,
                Points = dto.Points.Select(p => new LessonPoint{ HouseId = p.HouseId}).ToList()
            };

            Db.Lessons.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.Lessons.Select(ToDto()).SingleAsync(l => l.Id == entity.Id);

            Hub.Clients.All.Add(dto);

            return CreatedAtRoute("DefaultApi", new {id = dto.Id}, dto);
        }

        public async Task<LessonDto> PutLesson(LessonDto dto)
        {
            foreach (var point in dto.Points)
            {
                var ep = Db.LessonPoints.Single(l => l.Id == point.Id);
                ep.Amount = point.Amount;
            }

            await Db.SaveChangesAsync();

            dto = await Db.Lessons.Select(ToDto()).SingleAsync(l => l.Id == dto.Id);
            
            Hub.Clients.All.Update(dto);

            return dto;
        }

        public async Task<IHttpActionResult> DeleteLesson(int id)
        {
            var entity = await Db.Lessons.SingleAsync(l => l.Id == id);
            Db.Lessons.Remove(entity);

            await Db.SaveChangesAsync();
            
            Hub.Clients.All.Remove(id);
            
            return Ok();
        }

        private Expression<Func<Lesson, LessonDto>> ToDto()
        {
            return l => new LessonDto
            {
                Id = l.Id,
                Name = l.Name,
                TotalPoints = l.TotalPoints,
                Points = l.Points.Select(p => new LessonPointDto
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