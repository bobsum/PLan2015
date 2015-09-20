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

        public async Task<IHttpActionResult> PostEvent(PunctualityDto dto)
        {
            var entity = new Punctuality
            {
                Name = dto.Name,
                Deadline = dto.Deadline,
                All = dto.All
            };

            Db.Punctualities.Add(entity);
            await Db.SaveChangesAsync();

            dto = await Db.Punctualities.Select(ToDto()).SingleAsync(p => p.Id == dto.Id);
            
            Hub.Clients.All.Add(dto);
            
            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
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

        private void CalcPunctualityPoint()
        {
            //todo Håndtere At det kun er bestemt kolegier
            //Evt. kun tildele +points da dette nemmere kan gøres automatisk 
            var houses = Db.Houses.Where(h => h.Scouts.Any()).ToList();
            var scouts = Db.Scouts.ToList();

            var puns = Db.Punctualities
            .Where(p => p.Deadline < DateTime.Now)
            .Select(p => new { p, Scouts = p.Swipes.Where(s => s.Time < p.Deadline).Select(s => s.Scout).Distinct() })
            .ToList();

            foreach (var pun in puns)
            {
                var ss = pun.Scouts;
                if (pun.p.All)
                {
                    //ok
                    var h1 = scouts.Except(ss).Select(s => s.House).Distinct(); //.Dump("All Missing");
                    houses.Except(h1); //.Dump("All Ok");
                }
                else
                {
                    var h2 = ss.Select(s => s.House).Distinct(); //.Dump("Single Ok");
                    houses.Except(h2); //.Dump("Single Missing");
                }
            }
        }
    }
}