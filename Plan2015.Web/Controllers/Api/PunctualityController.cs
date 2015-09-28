using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using Plan2015.Data.Entities;
using Plan2015.Dtos;

namespace Plan2015.Web.Controllers.Api
{
    public class PunctualityController : ApiControllerWithHub<PunctualityHub>
    {
        public async Task<IEnumerable<PunctualityDto>> GetPunctualities()
        {
            return await Db.Punctualities.Select(ToDto()).ToListAsync();
        }

        public async Task<IHttpActionResult> PostPunctuality(PunctualityDto dto)
        {
            var entity = new Punctuality
            {
                Name = dto.Name,
                Deadline = dto.Deadline,
                All = dto.All
            };

            Db.Punctualities.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.Punctualities.Select(ToDto()).SingleAsync(p => p.Id == entity.Id);
            
            Hub.Clients.All.Add(dto);
            
            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }

        public async Task<IHttpActionResult> DeletePunctuality(int id)
        {
            var entity = await Db.Punctualities.SingleAsync(l => l.Id == id);
            Db.Punctualities.Remove(entity);

            await Db.SaveChangesAsync();

            Hub.Clients.All.Remove(id);
            //todo Call Score Hub
            return Ok();
        }

        private Expression<Func<Punctuality, PunctualityDto>> ToDto()
        {
            return p => new PunctualityDto
            {
                Id = p.Id,
                Name = p.Name,
                Deadline = p.Deadline,
                All = p.All
            };
        }
    }

    public class ScoreController : ApiControllerWithDB
    {
        public void GetScore()
        {
            var points = Db.PunctualityPoints.Select(p => new
            {
                House = p.House,
                Amount = 5
            }).Concat(
                Db.ActivityPoints.Select(p => new
                {
                    House = p.House,
                    Amount = p.Amount
                })).Concat(
                    Db.TurnoutPoints.Where(p => p.TeamMember != null).Select(p => new
                    {
                        House = p.House,
                        Amount = p.Amount
                    })).GroupBy(p => p.House, p => p.Amount, (h, a) => new
                    {
                        HouseId = h.Id,
                        HouseName = h.Name,
                        Amount = a.Sum()
                    });
        }
    }
}